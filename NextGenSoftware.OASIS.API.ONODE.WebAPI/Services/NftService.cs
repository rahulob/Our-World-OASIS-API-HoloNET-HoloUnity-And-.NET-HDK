﻿using System;
using System.Threading.Tasks;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.OASIS.API.ONODE.WebAPI.Interfaces;
using NextGenSoftware.OASIS.API.Providers.SOLANAOASIS.Infrastructure.Services.Solana;

namespace NextGenSoftware.OASIS.API.ONODE.WebAPI.Services
{
    public class NftService : INftService
    {
        private readonly ISolanaService _solanaService;
        private readonly ICargoService _cargoService;
        
        public NftService(ISolanaService solanaService, ICargoService cargoService)
        {
            _solanaService = solanaService;
            _cargoService = cargoService;
        }

        public async Task<OASISResult<NftTransactionRespone>> CreateNftTransaction(CreateNftTransactionRequest request)
        {
            var response = new OASISResult<NftTransactionRespone>();
            try
            {
                var nftTransaction = new NftTransactionRespone();
                switch (request.NftProvider)
                {
                    case NftProvider.Cargo:
                        var cargoPurchaseResponse = await _cargoService.PurchaseCargoSale(request.CargoExchange);
                        if (cargoPurchaseResponse.IsError)
                        {
                            response.IsError = true;
                            response.Message = cargoPurchaseResponse.Message;
                            return response;
                        }
                        nftTransaction.TransactionResult = cargoPurchaseResponse.Result.TransactionHash;
                        break;
                    case NftProvider.Solana:
                        var exchangeTokensResponse = await _solanaService.ExchangeTokens(request.SolanaExchange);
                        if (exchangeTokensResponse.Code != 200)
                        {
                            response.IsError = true;
                            response.Message = exchangeTokensResponse.Message;
                            return response;
                        }
                        nftTransaction.TransactionResult = exchangeTokensResponse.Payload.TransactionResult;
                        break;
                }
                response.IsError = false;
            }
            catch (Exception e)
            {
                response.IsError = true;
                response.Exception = e;
                response.Message = e.Message;
            }
            return response;
        }
    }
}