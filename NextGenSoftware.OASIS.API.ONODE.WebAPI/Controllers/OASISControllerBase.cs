﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Interfaces;
using NextGenSoftware.OASIS.API.DNA;

namespace NextGenSoftware.OASIS.API.ONODE.WebAPI.Controllers
{
    public class OASISControllerBase : ControllerBase
    {
       // public IOptions<OASISSettings> OASISSettings;

        public IAvatar Avatar
        {
            get
            {
                if (HttpContext.Items.ContainsKey("Avatar") && HttpContext.Items["Avatar"] != null)
                    return (IAvatar)HttpContext.Items["Avatar"];

                //if (HttpContext.Session.GetString("Avatar") != null)
                //    return JsonSerializer.Deserialize<IAvatar>(HttpContext.Session.GetString("Avatar"));

                return null;
            }
            set
            {
                HttpContext.Items["Avatar"] = value;
                //HttpContext.Session.SetString("Avatar", JsonSerializer.Serialize(value));
            }
        }

        //public OASISControllerBase(IOptions<OASISSettings> settings)
        public OASISControllerBase()
        {
            //OASISSettings = settings;
           // OASISProviderManager.OASISSettings = settings.Value;
        }

        //TODO: REMOVE ASAP, NOT USED ANYMORE
        //public OASISControllerBase(IOptions<OASISDNA> settings)
        //{
        //    //OASISSettings = settings;
        //    // OASISProviderManager.OASISSettings = settings.Value;
        //}

        protected IOASISStorage GetAndActivateDefaultProvider()
        {
            return OASISDNAManager.GetAndActivateDefaultProvider();
        }

        protected IOASISStorage GetAndActivateProvider(ProviderType providerType, bool setGlobally = false)
        {
            return OASISDNAManager.GetAndActivateProvider(providerType, null, false, setGlobally);
        }

        protected IOASISStorage GetAndActivateProvider(ProviderType providerType, string customConnectionString = null, bool forceRegister = false, bool setGlobally = false)
        {
            return OASISDNAManager.GetAndActivateProvider(providerType, customConnectionString, forceRegister, setGlobally);
        }
    }
}
