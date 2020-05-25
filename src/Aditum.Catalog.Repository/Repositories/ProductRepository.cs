using Aditum.Catalog.Repository.Entities;
using MongoDB.Driver;
using MongoDbGenericRepository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aditum.Catalog.Repository.Repositories
{
    /// <summary>
    /// Responsible for managing objects in the database.
    /// </summary>
    public class ProductRepository
    {
        private readonly IBaseMongoRepository mongoRepository;

        /// <summary>
        /// Inserts the product into the database.
        /// </summary>
        public ProductEntity Insert(ProductEntity newProduct)
        {
            this.mongoRepository.AddOne(newProduct);

            return newProduct;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        public List<ProductEntity> GetAll()
        {
            return this.mongoRepository.GetAll<ProductEntity>(p => p.Id != null);    
        }

        /// <summary>
        /// Get's a product by it's ID.
        /// </summary>
        public ProductEntity GetById(Guid productId)
        {
            // var filter = Builders<ProductEntity>.Filter.Eq("ProductId", productId);

            return this.mongoRepository.GetOne<ProductEntity>(p => p.Id.Equals(productId));
        }

        /// <summary>
        /// Updates only filled properties of a product, by it's ID.
        /// </summary>
        public ProductEntity UpdateById(Guid productId, ProductEntity updatedProduct)
        {
            var currentProduct = this.mongoRepository.GetOne<ProductEntity>(p => p.Id == productId);

            var inputProduct = Builders<ProductEntity>.Update
                .Set(p => p.Name, updatedProduct.Name)
                .Set(p => p.Description, updatedProduct.Description)
                .Set(p => p.Amount, updatedProduct.Amount)
                .Set(p => p.Weight, updatedProduct.Weight)
                .Set(p => p.Hight, updatedProduct.Hight)
                .Set(p => p.Width, updatedProduct.Width);
            
            if (this.mongoRepository.UpdateOne<ProductEntity>(currentProduct, inputProduct))
            {
                return this.mongoRepository.GetById<ProductEntity>(productId);
            } else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Deletes a product by it's ID.
        /// </summary>
        public void DeleteById(Guid productId)
        {
            this.mongoRepository.DeleteOne<ProductEntity>(p => p.Id == productId);
        }

        public ProductRepository()
        {
            this.mongoRepository = new MongoRepository(
                "mongodb+srv://luizfelipe:luizfelipe01@cluster0-ssdoc.mongodb.net/messagers?authSource=admin&replicaSet=Cluster0-shard-0&readPreference=primary&appname=MongoDB%20Compass&ssl=true",
                // "mongodb://localhost:27017",
                "ProductCatalog");
        }
    }
}
