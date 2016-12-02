using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Code4Cash.Data.Databse;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.ViewModels.Base;

namespace Code4Cash.Controllers.Base
{
    public class GenericController<TEm, TE, TVm> : ApiController
        where TE : Entity
        where TVm : ViewModel
        where TEm : EntityMap<TE, TVm>
    {
        private readonly Database _database;

        public GenericController()
        {
            _database = new Database();
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<ViewModel>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var allE = await _database.Repo<TE>().All();

            var allVm = Mapper.Map<IEnumerable<TVm>>(allE);

            return Ok(allVm);
        }

        [HttpGet]
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> Get([FromUri] string id)
        {
            var entity = await _database.Repo<TE>().GetOne(id);
            if (entity == null)
            {
                return NotFound();
            }
            var viewModel = Mapper.Map<TVm>(entity);
            return Ok(viewModel);
        }

        public async Task<IHttpActionResult> Add([FromBody] TVm viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<TE>(viewModel);
            var repo = _database.Repo<TE>();
            entity = await repo.Add(entity);
            viewModel = Mapper.Map<TVm>(entity);
            return Created("", viewModel);
        }

    }
}