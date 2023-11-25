
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataAccess.Data;

namespace Business
{
    /// <summary>
    /// La clase CustomerService hereda de BaseService y se encarga de la lógica de negocio relacionada con los Customers.
    /// </summary>
    public class CustomerService : BaseService<Customer>
    {
        private CustomerModel _customerModel;
        private readonly PostService _postService;

        /// <summary>
        /// Constructor de la clase CustomerService.
        /// </summary>
        /// <param name="customerModel">Una instancia de CustomerModel que representa el modelo de datos de un Customer.</param>
        /// <param name="postService">Una instancia de PostService que representa el servicio de Posts.</param>
        public CustomerService(CustomerModel customerModel, PostService postService) : base(customerModel)
        {
            _customerModel = customerModel;
            _postService = postService;
        }

        /// <summary>
        /// Obtiene un Customer por su nombre.
        /// </summary>
        /// <param name="name">El nombre del Customer a buscar.</param>
        /// <returns>El Customer encontrado, o null si no se encuentra ningún Customer con ese nombre.</returns>
        public virtual Customer GetByName(string name)
        {
            return _customerModel.FindByName(name);
        }



    }



}
