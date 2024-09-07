using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Product;
using BooksmartAPI.Models;
using Mapster;

namespace BooksmartAPI.Services
{
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetProductDTO> GetDTO(string barcode)
        {
            Product product = await _unitOfWork.Products.GetWithAllRelations(barcode);
            GetProductDTO getProductDTO = new();
            var config = new TypeAdapterConfig();
            config.NewConfig<Product, GetProductDTO>()
                .Map(dest => dest.Name, src => src.BarCode.Name)
                .Map(dest => dest.BarCode, src => src.BarCode.Value)
                .Map(dest => dest.Description, src => src.BarCode.Description)
                .Map(dest => dest.Pages, src => src.BarCode.Pages)
                .Map(dest => dest.Author, src => src.BarCode.Author.Name)
                .Map(dest => dest.Category, src => src.BarCode.Category.Name)
                .Map(dest => dest.Genres, src => src.BarCode.Genres)
                .Map(dest => dest.Count, src => _unitOfWork.Products.GetAllByBarcode(barcode).Result.Count());
            TypeAdapter.Adapt(product, getProductDTO, typeof(Product), typeof(GetProductDTO), config);

            return getProductDTO;
        }


        //Missing brainwork to work
        public async Task<List<GetAllProductDTO>> GetAllDTO()
        {
            IEnumerable<Product> products = await _unitOfWork.Products.GetAllAsync();
            List<GetAllProductDTO> getAllProductDTO = new();

            var config = new TypeAdapterConfig();
            config.NewConfig<Product, GetAllProductDTO>()
                .Map(dest => dest.Name, src => src.BarCode.Name);
            TypeAdapter.Adapt(products, getAllProductDTO, typeof(Product), typeof(GetProductDTO), config);

            return getAllProductDTO;
        }

        public async Task<bool> CreateWithBarcode(string BarCode)
        {
            Barcode barcode = await _unitOfWork.Barcodes.GetBarcodeByBarcode(BarCode);

            Product product = new() { BarCode =  barcode};

            await _unitOfWork.Products.AddAsync(product);
            return true;
        }

        public async Task<bool> CreateNew(CreateProductDTO createProductDTO)
        {
            Barcode barcode = new();
            var config = new TypeAdapterConfig();
            config.NewConfig<CreateProductDTO, Barcode>()
                .Map(dest => dest.Author, src => _unitOfWork.Authors.GetAuthorByName(createProductDTO.Author))
                .Map(dest => dest.Category, src => _unitOfWork.Categories.GetCategoryByName(createProductDTO.Category))
                .Ignore(dest => dest.Genres);
            TypeAdapter.Adapt(createProductDTO, barcode, typeof(CreateProductDTO), typeof(Barcode), config);

            foreach (var item in createProductDTO.Genres) {
                Genre genre = await _unitOfWork.Genres.GetByName(item.Name);
                if(genre is null) genre.Name = item.Name;

                barcode.Genres.Add(genre);
            }

            await _unitOfWork.Barcodes.AddAsync(barcode);

            Product product = new() { BarCode = barcode};

            await _unitOfWork.Products.AddAsync(product);
            return true;
        }
    }
}
