using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //private readonly IProductRepository _repos;

        // public ProductsController(IProductRepository repos)
        // {
        //     _repos = repos;
        // }
        private readonly IGenericRepository<Product> _productrepo;
        private readonly IGenericRepository<ProductBrand> _productbrandrepo;
        private readonly IGenericRepository<ProductType> _productTyperepo;
        private readonly IMapper _mapper; 
        public ProductsController(IGenericRepository<Product> productrepo, IGenericRepository<ProductBrand> productbrandrepo, IGenericRepository<ProductType> productTyperepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTyperepo = productTyperepo;
            _productbrandrepo = productbrandrepo;
            _productrepo = productrepo;

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productrepo.ListAsync(spec);

            // return products.Select(p => new ProductToReturnDto
            // {
            //     Id = p.Id,
            //     Name = p.Name,
            //     Description = p.Description,
            //     Price = p.Price,
            //     PictureUrl = p.PictureUrl,
            //     ProductType = p.ProductType.Name,
            //     ProductBrand = p.ProductBrand.Name
            // }).ToList();

            return Ok(_mapper
            .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productrepo.GetEntityWithSpec(spec);

            // return new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductType = product.ProductType.Name,
            //     ProductBrand = product.ProductBrand.Name
            // };

            return _mapper.Map<Product,ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productbrandrepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTyperepo.ListAllAsync());
        }
    }
}