using Aditum.Catalog.Model;
using Aditum.Catalog.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aditum.Catalog.WebApi.Controllers
{
    /// <summary>
    /// Responsible for handling Products.
    /// </summary>
    [Route("api/1/catalog")]
    public class CatalogController : Controller
    {
        ProductRepository productRepository = new ProductRepository();

        // Create new product
        [HttpPost]
        public IActionResult Post([FromBody] Product newProduct)
        {
            try
            {
                // Validate product data:
                if (newProduct == null)
                {
                    return this.BadRequest(new Error { Code = 1, Message = "Missing product data" });
                }
                else
                {
                    if (newProduct.Amount <= 0)
                    {
                        return this.BadRequest(new Error { Code = 2, Message = "Invalid product amount" });
                    }
                    if (string.IsNullOrEmpty(newProduct.Name) == true)
                    {
                        return this.BadRequest(new Error { Code = 2, Message = "Invalid product name" });
                    }
                }

                // Insert product into the database:
                var insertedProduct = this.productRepository.Insert(new Repository.Entities.ProductEntity
                {
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Amount = newProduct.Amount,
                    Hight = newProduct.Hight,
                    Weight = newProduct.Weight,
                    Width = newProduct.Width
                });

                // Return the created product:
                return this.Ok(new
                {
                    Success = true,
                    insertedProduct.Id,
                    newProduct,
                });
            }
            catch (Exception ex)
            {
                // Returns 500 if an unexpected exception occurres:
                Console.WriteLine(ex.Message);
                return base.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // Read all products
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = new List<Product>();
                var getProducts = this.productRepository.GetAll();

                foreach (var product in getProducts)
                {
                    products.Add(new Product{
                        Name = product.Name,
                        Description = product.Description,
                        Amount = product.Amount,
                        Weight = product.Weight,
                        Hight = product.Hight,
                        Width = product.Width
                    });
                }

                return Ok(new
                {
                    Success = true,
                    products
                });
            }
            catch (Exception)
            {
                // Returns 500 if an unexpected exception occurres:
                return base.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // Read product
        [HttpGet("{productId}")]
        public IActionResult Get(Guid productId)
        {
            try
            {
                var getProduct = this.productRepository.GetById(productId);

                var product = new Product {
                    Name = getProduct.Name,
                    Description = getProduct.Description,
                    Amount = getProduct.Amount,
                    Weight = getProduct.Weight,
                    Hight = getProduct.Hight
                };

                return Ok(new
                {
                    Success = true,
                    getProduct.Id,
                    product
                });
            }
            catch (Exception)
            {
                // Returns 500 if an unexpected exception occurres:
                return base.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // Update
        [HttpPut("{productId}")]
        public IActionResult Put(Guid productId, [FromBody] Product updatedProduct)
        {
            try
            {
                // Validate product data:
                if (updatedProduct == null)
                {
                    return this.BadRequest(new Error { Code = 1, Message = "Missing product data" });
                }
                else
                {
                    if (updatedProduct.Amount <= 0)
                    {
                        return this.BadRequest(new Error { Code = 2, Message = "Invalid product amount" });
                    }
                    if (string.IsNullOrEmpty(updatedProduct.Name) == true)
                    {
                        return this.BadRequest(new Error { Code = 2, Message = "Invalid product name" });
                    }
                }

                // Update product into the database:
                var insertedUpdatedProduct = this.productRepository.UpdateById(productId, new Repository.Entities.ProductEntity
                {
                    Amount = updatedProduct.Amount,
                    Description = updatedProduct.Description,
                    Hight = updatedProduct.Hight,
                    Name = updatedProduct.Name,
                    Weight = updatedProduct.Weight,
                    Width = updatedProduct.Width
                });

                // Return the updated product:
                return this.Ok(new
                {
                    Success = true,
                    insertedUpdatedProduct.Id,
                    updatedProduct,
                });
            }
            catch (Exception)
            {
                // Returns 500 if an unexpected exception occurres:
                return base.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // Delete
        [HttpDelete("{productId}")]
        public IActionResult Delete(Guid productId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.productRepository.DeleteById(productId);

                    return Ok(new
                    {
                        Success = true,
                        productId
                    });

                }else
                {
                    return this.BadRequest(new Error { Code = 3, Message = $"The value {productId} is not valid." });
                }
                
            }
            catch (Exception)
            {
                // Returns 500 if an unexpected exception occurres:
                return base.StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
