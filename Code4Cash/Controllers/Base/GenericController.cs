using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Code4Cash.Data.Access;
using Code4Cash.Data.Base;
using Code4Cash.Data.Database;
using Code4Cash.Data.Models.Entities.Base;
using Code4Cash.Data.Models.Entities.Users;
using Code4Cash.Data.Models.ModelMappings.Base;
using Code4Cash.Data.Models.RequestModels;
using Code4Cash.Data.Models.ViewModels.Base;
using Code4Cash.Misc;
using Code4Cash.Misc.Exceptions;
using Code4Cash.Misc.Filters;

namespace Code4Cash.Controllers.Base
{
    public class GenericController<TE, TVm> : ApiController
        where TE : Entity
        where TVm : ViewModel
    {

        private IDataLayer _databaseLayer;

        public GenericController()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _databaseLayer?.Dispose();
                _databaseLayer = null;
            }
            base.Dispose(disposing);
        }

        private HttpRequestMessage _request = null;

        private void InitDataLayer()
        {
            if (_databaseLayer != null || Request == null) return;
            var accountId = AuthHelper.GetInstance().GetAccountId(Request);
            _databaseLayer = new DataAccessLayer(accountId);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<ViewModel>))]
        [Auth(RoleFunction.None)]
        public virtual async Task<IHttpActionResult> GetAll()
        {
            InitDataLayer();
            var allE = await Repo.All();

            var allVm = Mapper.Map<IEnumerable<TVm>>(allE);

            return Ok(allVm);
        }

        [HttpGet]
        [ResponseType(typeof(ViewModel))]
        [Auth(RoleFunction.None)]
        public virtual async Task<IHttpActionResult> Get([FromUri] string id)
        {
            InitDataLayer();
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
            InitDataLayer();
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
            InitDataLayer();
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
            catch (Exception exc)
            {
                return InternalServerError(exc);
            }
        }

        protected IGenericRepository<TE> Repo => _databaseLayer.Repo<TE>();
    }
}