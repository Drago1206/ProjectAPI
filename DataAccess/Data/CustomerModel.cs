
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{

    /// <summary>
    /// Modelo para gestionar las operaciones de la entidad Customer.
    /// </summary>
    public class CustomerModel : BaseModel<Customer>
    {
        /// <summary>
        /// Constructor que inicializa el modelo base con la entidad Customer.
        /// </summary>
        /// <param name="context">El contexto de la base de datos.</param>
        public CustomerModel(syscomTestContext context) : base(context)
        {
        }

        /// <summary>
        /// Busca un cliente por su nombre.
        /// </summary>
        /// <param name="name">El nombre del cliente a buscar.</param>
        /// <returns>El cliente encontrado o null si no se encuentra ningún cliente con ese nombre.</returns>
        public Customer FindByName(string name)
        {
            return _dbSet.FirstOrDefault(e => e.Name == name);
            var customer = _dbSet.FirstOrDefault(e => e.Name == name);

            if (customer == null)
            {
                // se lanza una excepción  cuando el cliente no existe.
                throw new Exception("Cliente no encontrado");
            }

            return customer;
        }

    }


}