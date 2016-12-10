using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc.Exceptions;
using Code4Cash.Misc.Filters;

namespace Code4Cash.Controllers.Base
{
    public class GenericController<TE, TVm> : ApiController
        where TE : Entity
        where TVm : ViewModel
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
        [Auth(RoleFunction.None)]
        public virtual async Task<IHttpActionResult> GetAll()
        {
            var allE = await Repo.All();

            var allVm = Mapper.Map<IEnumerable<TVm>>(allE);

            return Ok(allVm);
        }

        [HttpGet]
        [ResponseType(typeof(ViewModel))]
        [Auth(RoleFunction.None)]
        public virtual async Task<IHttpActionResult> Get([FromUri] string id)
        {
            var entity = await Repo.GetOneBySelector(id);
            if (entity == null)
            {
                return NotFound();
            }
            var viewModel = Mapper.Map<TVm>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        [ResponseType(typeof(ViewModel))]
        [Auth(RoleFunction.CanAddNewEntities)]
        public virtual async Task<IHttpActionResult> Add([FromBody] RequestModel<TVm> requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var viewModel = requestModel.Data;
            var entity = Mapper.Map<TE>(viewModel);

            entity = await Repo.Add(entity);
            viewModel = Mapper.Map<TVm>(entity);
            return Created("", viewModel);
        }

        [HttpPost]
        [ResponseType(typeof(ViewModel))]
        [Auth(RoleFunction.CanUpdateEntities)]
        public virtual async Task<IHttpActionResult> Update([FromUri] string id, [FromBody] RequestModel<TVm> requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var viewModel = requestModel.Data;
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

        protected Repository<TE> Repo => _databaseUnit.Repo<TE>();
    }
}