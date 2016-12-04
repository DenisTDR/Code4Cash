using System;
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
using Code4Cash.Misc.Exceptions;

namespace Code4Cash.Controllers.Base
{
    public class GenericController<TE, TVm> : ApiController
        where TE : Entity
        where TVm : ViewModel
//        where TEm : EntityViewModelMap<TE, TVm>
    {
        private DatabaseUnit _databaseUnit;

        public GenericController()
        {
            _databaseUnit = new DatabaseUnit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseUnit?.Dispose();
                _databaseUnit = null;
            }
            base.Dispose(disposing);
        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<ViewModel>))]
        public async Task<IHttpActionResult> GetAll()
        {
            var allE = await Repo.All();

            var allVm = Mapper.Map<IEnumerable<TVm>>(allE);

            return Ok(allVm);
        }

        [HttpGet]
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> Get([FromUri] string id)
        {
            var entity = await Repo.GetOne(id);
            if (entity == null)
            {
                return NotFound();
            }
            var viewModel = Mapper.Map<TVm>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        [ResponseType(typeof(ViewModel))]
        public async Task<IHttpActionResult> Add([FromBody] TVm viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = Mapper.Map<TE>(viewModel);

            entity = await Repo.Add(entity);
            viewModel = Mapper.Map<TVm>(entity);
            return Created("", viewModel);
        }

        public async Task<IHttpActionResult> Update([FromUri] string id, [FromBody] TVm viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var entity = Mapper.Map<TE>(viewModel);
                entity = await Repo.Update(id, entity);

                viewModel = Mapper.Map<TVm>(entity);
                return Ok(viewModel);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private Repository<TE> Repo => _databaseUnit.Repo<TE>();
    }
}