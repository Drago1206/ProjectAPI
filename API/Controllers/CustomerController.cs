using Business;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CustomerEntity = DataAccess.Data.Customer;

namespace API.Controllers.Customer
{
    [Route("[controller]")]
    /// <summary>
    /// Controlador para gestionar las operaciones CRUD de CustomerEntity.
    /// </summary>
    public class CustomerController : ControllerBase
    {
        private BaseService<CustomerEntity> _customerServiceBase;
        private CustomerService _customerServiceSpecific;
        private readonly PostService _postService;

        /// <summary>
        /// Constructor que inicializa los servicios para la entidad CustomerEntity.
        /// </summary>
        /// <param name="customerServiceBase">El servicio base para la entidad CustomerEntity.</param>
        /// <param name="customerServiceSpecific">El servicio específico para la entidad CustomerEntity.</param>
        public CustomerController(BaseService<CustomerEntity> customerServiceBase, CustomerService customerServiceSpecific, PostService postService)
        {
            _customerServiceBase = customerServiceBase;
            _customerServiceSpecific = customerServiceSpecific;
            _postService = postService;
        }

        /// <summary>
        /// Obtiene todas las entidades CustomerEntity.
        /// </summary>
        /// <returns>Una consulta IQueryable de todas las entidades CustomerEntity.</returns>
        [HttpGet()]
        public IQueryable<CustomerEntity> GetAll()
        {
            return _customerServiceBase.GetAll();
        }

        /// <summary>
        /// Crea una entidad CustomerEntity.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a crear.</param>
        /// <returns>La entidad CustomerEntity creada.</returns>
        [HttpPost()]
        public IActionResult CreateCustomer([FromBody] CustomerEntity entity)
        {
            try
            {
                var createdCustomer = CreateCustomer(entity);
                return Ok(createdCustomer);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Método privado para crear una entidad CustomerEntity.
        /// Comprueba si la entidad es nula y si ya existe un cliente con el mismo nombre.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a crear.</param>
        /// <returns>La entidad CustomerEntity creada.</returns>
        private CustomerEntity CrearCliente(CustomerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var existingCustomer = _customerServiceSpecific.GetByName(entity.Name);

            if (existingCustomer != null)
            {
                throw new Exception("Ya existe un cliente con el mismo nombre.");
            }
            return _customerServiceSpecific.Create(entity);
        }

        /// <summary>
        /// Actualiza una entidad CustomerEntity existente.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a actualizar.</param>
        /// <returns>La entidad CustomerEntity actualizada.</returns>
        [HttpPut()]
        public CustomerEntity Actualizar(CustomerEntity entity)
        {
            return _customerServiceBase.Update(entity.CustomerId, entity, out bool cambioRealizado);
        }

        /// <summary>
        /// Elimina una entidad CustomerEntity existente.
        /// </summary>
        /// <param name="entity">La entidad CustomerEntity a eliminar.</param>
        /// <returns>La entidad CustomerEntity eliminada.</returns>
        [HttpDelete()]
        public CustomerEntity Eliminar([FromBodyAttribute] CustomerEntity entity)
        {
            _postService.DeletePostsByCustomerId(entity.CustomerId);

            // Luego de eliminar los posts, se puede proceder a eliminar al cliente
            return _customerServiceBase.Delete(entity);
        }
    }
}