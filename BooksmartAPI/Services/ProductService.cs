using BooksmartAPI.Data;
using BooksmartAPI.DTOs.Product;
using BooksmartAPI.Hubs;
using BooksmartAPI.Models;
using Mapster;

namespace BooksmartAPI.Services
{
    public class ProductService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly StoreHub _storeHub;

        public ProductService(UnitOfWork unitOfWork, StoreHub storeHub)
        {
            _unitOfWork = unitOfWork;
            _storeHub = storeHub;
        }

        public async Task<GetProductDTO> GetDTO(string barcode)
        {
            Product product = await _unitOfWork.Products.GetWithAllRelations(barcode);
            if (product is null) return null;
            GetProductDTO getProductDTO = new();
            var config = new TypeAdapterConfig();
            config.NewConfig<Product, GetProductDTO>()
                .Map(dest => dest.Name, src => src.BarCode.Name)
                .Map(dest => dest.BarCode, src => src.BarCode.Value)
                .Map(dest => dest.Description, src => src.BarCode.Description)
                .Map(dest => dest.Pages, src => src.BarCode.Pages)
                .Map(dest => dest.Author, src => src.BarCode.Author.Name)
                .Map(dest => dest.Category, src => src.BarCode.Category.Name)
                .Map(dest => dest.Price, src => src.BarCode.Price)
                .Map(dest => dest.RentPrice, src => src.BarCode.RentPrice)
                .Map(dest => dest.Genres, src => MapGenres(src.BarCode.Genres))
                .Map(dest => dest.Count, src => _unitOfWork.Products.GetAllByBarcode(barcode).Result.Count());
            TypeAdapter.Adapt(product, getProductDTO, typeof(Product), typeof(GetProductDTO), config);

            await _storeHub.Test(product);

            return getProductDTO;
        }

        public async Task<IEnumerable<GetAllProductDTO>> GetAllDTO()
        {
            IEnumerable<Product> products = await _unitOfWork.Products.GetAllWithAllRelations();

            var config = new TypeAdapterConfig();
            config.NewConfig<Product, GetAllProductDTO>()
                .Map(dest => dest.Name, src => src.BarCode.Name)
                .Map(dest => dest.Author, src => src.BarCode.Author.Name)
                .Map(dest => dest.Category, src => src.BarCode.Category.Name)
                .Map(dest => dest.Barcode, src => src.BarCode.Value)
                .Map(dest => dest.Genres, src => MapGenres(src.BarCode.Genres));

            var getAllProductDTO = products.Adapt<IEnumerable<GetAllProductDTO>>(config);

            getAllProductDTO = getAllProductDTO.DistinctBy(x => x.Barcode);

            return getAllProductDTO;
        }
        private List<string> MapGenres(List<Genre> genres)
        {
            List<string> genreStrings = new();

            foreach (var genre in genres)
            {
                genreStrings.Add(genre.Name);
            }

            return genreStrings;
        }

        public async Task<bool> CreateWithBarcode(string BarCode)
        {
            Barcode barcode = await _unitOfWork.Barcodes.GetBarcodeByBarcode(BarCode);

            Product product = new() { BarCode =  barcode };

            await _unitOfWork.Products.AddAsync(product);
            await _storeHub.SendUpdate(product);
            return true;
        }

        public async Task<bool> CreateNew(CreateProductDTO createProductDTO)
        {
            Barcode barcode = new();
            var config = new TypeAdapterConfig();
            config.NewConfig<CreateProductDTO, Barcode>()
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Value, src => src.BarCodeValue)
                .Ignore(dest => dest.Author)
                .Ignore(dest => dest.Category)
                .Ignore(dest => dest.Genres)
                .Ignore(dest => dest.Id);
            TypeAdapter.Adapt(createProductDTO, barcode, typeof(CreateProductDTO), typeof(Barcode), config);

            foreach (var item in createProductDTO.Genres) {
                Genre genre = await _unitOfWork.Genres.GetByName(item);
                if(genre is null) genre = new() { Name = item };

                barcode.Genres.Add(genre);
            }

            Author author = await _unitOfWork.Authors.GetAuthorByName(createProductDTO.Author);
            if (author is null) author = new() { Name = createProductDTO.Author };

            barcode.Author = author;

            Category category = await _unitOfWork.Categories.GetCategoryByName(createProductDTO.Category);
            if (category is null) category = new() { Name = createProductDTO.Category };

            barcode.Category = category;

            await _unitOfWork.Barcodes.AddAsync(barcode);

            Product product = new() { BarCode = barcode};

            await _unitOfWork.Products.AddAsync(product);
            await _storeHub.SendUpdate(product);
            return true;
        }
    }
}
