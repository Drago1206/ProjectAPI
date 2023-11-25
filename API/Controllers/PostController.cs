using Business;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostEntity = DataAccess.Data.Post;

namespace API.Controllers.Post
{
    [Route("[controller]")]
    /// Controlador para las operaciones CRUD de PostEntity.
    public class PostController : ControllerBase
    {
        private BaseService<PostEntity> _postServiceBase;
        private PostService _postServiceSpecific;

        /// <summary>
        /// Constructor que inicializa el servicio base con la entidad PostEntity.
        /// </summary>
        /// <param name="postService">El servicio base para la entidad PostEntity.</param>
        public PostController(BaseService<PostEntity> postService, PostService postServiceSpecific)
        {
            _postServiceBase = postService;
            _postServiceSpecific = postServiceSpecific;
        }

        /// <summary>
        /// Obtiene todas las entidades PostEntity.
        /// </summary>
        /// <returns>Una consulta IQueryable de todas las entidades PostEntity.</returns>
        [HttpGet()]
        public async Task<IActionResult> ObtenerTodos()
        {
            var posts = await _postServiceBase.GetAllAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Crea varias entidades PostEntity en una sola petición.
        /// </summary>
        /// <param name="entities">La lista de entidades PostEntity a crear.</param>
        /// <returns>Una lista de las entidades PostEntity creadas.</returns>
        [HttpPost()]
        public List<PostEntity> Crear([FromBody] List<PostEntity> entities)
        {
            List<PostEntity> entidadesCreadas = new List<PostEntity>();
            foreach (var entidad in entities)
            {
                entidadesCreadas.Add(_postServiceSpecific.Create(entidad));
            }
            return entidadesCreadas;

         
        }

        /// <summary>
        /// Actualiza una entidad PostEntity.
        /// </summary>
        /// <param name="entity">La entidad PostEntity a actualizar.</param>
        /// <returns>La entidad PostEntity actualizada.</returns>
        [HttpPut()]
        public PostEntity Actualizar([FromBodyAttribute] PostEntity entity)
        {
            return _postServiceBase.Update(entity.PostId, entity, out bool cambioRealizado);

        }

        /// <summary>
        /// Elimina una entidad PostEntity.
        /// </summary>
        /// <param name="entity">La entidad PostEntity a eliminar.</param>
        /// <returns>La entidad PostEntity eliminada.</returns>
        [HttpDelete()]
        public PostEntity Eliminar([FromBodyAttribute] PostEntity entity)
        {
            return _postServiceBase.Delete(entity);
        }
    }
}