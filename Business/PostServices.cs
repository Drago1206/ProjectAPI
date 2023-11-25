using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Linq;

namespace Business
{
    /// <summary>
    /// La clase PostService hereda de BaseService y se encarga de la lógica de negocio relacionada con los Posts.
    /// </summary>
    public class PostService : BaseService<Post>
    {
        private BaseModel<Post> _post;
        private BaseModel<Customer> _customerModel;
        /// <summary>
        /// Constructor de la clase PostService.
        /// </summary>
        /// <param name="post">Una instancia de BaseModel<Post> que representa el modelo de datos de un Post.</param>
        public PostService(BaseModel<Post> post, BaseModel<Customer> customerModel) : base(post)
        {
            _customerModel = customerModel;
            _post = post;
        }

        /// <summary>
        /// Crea una nueva entidad Post.
        /// </summary>
        /// <param name="entity">La entidad Post a crear.</param>
        /// <returns>La entidad Post creada.</returns>
        /// <exception cref="Exception">Se lanza una excepción si el usuario asociado no existe.</exception>
        public override Post Create(Post entity)
        {
            // Validar que el usuario asociado exista
            var customer = _customerModel.FindById(entity.CustomerId);
            if (customer == null)
            {
                throw new Exception("El usuario no existe");
            }

           

            

            // Si el texto del Body es mayor a 100 caracteres se debe cortar el texto a 97 caracteres y finalizar agregar al final "..."
            if (entity.Body.Length > 100)
            {
                entity.Body = entity.Body.Substring(0, 97) + "...";
            }

            // Si EL Type es igual a 1 entonces Category = "Farándula"
            // Sino Si EL Type es igual a 2 entonces Category = "Política"
            // Sino Si EL Type es igual a 3 entonces Category = "Futbol" Sino dejar la que el usuario ingrese.
            switch (entity.Type)
            {
                case 1:
                    entity.Category = "Farándula";
                    break;
                case 2:
                    entity.Category = "Política";
                    break;
                case 3:
                    entity.Category = "Futbol";
                    break;
            }

            


            return base.Create(entity);
        }

        /// <summary>
        /// Elimina todos los Posts asociados a un Customer específico.
        /// </summary>
        /// <param name="customerId">El ID del Customer cuyos Posts se van a eliminar.</param>
        public void DeletePostsByCustomerId(int customerId)
        {
            // Obtiene todos los Posts que pertenecen al Customer con el ID especificado
            var posts = GetAll().Where(post => post.CustomerId == customerId).ToList();

            // Recorre cada Post encontrado y lo elimina
            foreach (var post in posts)
            {
                Delete(post);
            }
        }

    }


}