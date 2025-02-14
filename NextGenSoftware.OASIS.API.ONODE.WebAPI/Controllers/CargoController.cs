﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.OASIS.API.ONODE.WebAPI.Interfaces;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Infrastructure.Handlers.Commands;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Models.Cargo;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Models.Request;
using NextGenSoftware.OASIS.API.Providers.CargoOASIS.Models.Response;

namespace NextGenSoftware.OASIS.API.ONODE.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CargoController : OASISControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpPost]
        [Route("AuthorizeAccount")]
        public async Task<OASISResult<CreateAccountResponseModel>> AuthorizeAccount(
            [FromBody] CreateAccountRequestModel requestModel)
        {
            return await _cargoService.AuthorizeCargoAccount(requestModel);
        }

        [HttpPost]
        [Route("AuthenticateAccount")]
        public async Task<OASISResult<CreateAccountResponseModel>> AuthenticateAccount()
        {
            return await _cargoService.AuthenticateCargoAccount();
        }

        [HttpPost]
        [Route("PurchaseSale")]
        public async Task<OASISResult<PurchaseResponseModel>> PurchaseCargoSale([FromBody] PurchaseRequestModel requestModel)
        {
            return await _cargoService.PurchaseCargoSale(requestModel);
        }

        [HttpPost]
        [Route("CancelSale")]
        public async Task<OASISResult<CancelSaleResponseModel>> CancelCargoSale([FromBody] CancelSaleRequestModel requestModel)
        {
            return await _cargoService.CancelCargoSale(requestModel);
        }

        [HttpGet]
        [Route("GetOrders")]
        public async Task<OASISResult<PaginationResponseWithResults<IEnumerable<Order>>>> GetCargoOrders(
            [FromBody] OrderParams orderParams)
        {
            return await _cargoService.GetCargoOrders(orderParams);
        }

        [HttpGet]
        [Route("GetUserTokensByContract")]
        public async Task<OASISResult<GetUserTokensByContractResponseModel>> GetUserTokensByContract(
            [FromBody] GetUserTokensByContractRequestModel requestModel)
        {
            return await _cargoService.GetUserTokensByContract(requestModel);
        }

        [HttpGet]
        [Route("GetCollectiblesListByProjectId")]
        public async Task<OASISResult<GetCollectiblesListByProjectIdResponseModel>> CollectiblesListByProjectId(
            [FromBody] GetCollectiblesListByProjectIdRequestModel requestModel)
        {
            return await _cargoService.CollectiblesListByProjectId(requestModel);
        }
    }
}