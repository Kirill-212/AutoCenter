using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceNew.Services
{
    public class AsyncServiceNew : IAsyncServiceNew<New>
    {

        private readonly IAsyncHttpClientNew<New> httpClientNew;
        private readonly IAsyncHttpClientUserForNew<Employee> asyncHttpClientUser;
        private readonly IAsyncHttlClientImg<Img> httpClientImg;
        private readonly IMapper mapper;

        public AsyncServiceNew(IAsyncHttpClientNew<New> httpClientNew,
            IMapper mapper,
            IAsyncHttpClientUserForNew<Employee> asyncHttpClientUser,
            IAsyncHttlClientImg<Img> httpClientImg
            )
        {
            this.asyncHttpClientUser = asyncHttpClientUser;
            this.mapper = mapper;
            this.httpClientImg = httpClientImg;
            this.httpClientNew = httpClientNew;
        }

        public async Task<int> Create(NewWrapperDto<PostNewDto> item, string jwt = null)
        {
            New postNew = mapper.Map<New>(item.New);
            postNew.EmployeeId = (await asyncHttpClientUser.SetJwt(jwt).GetUserByEmail(item.New.Email)).Id;
            postNew.Imgs = mapper.Map<List<Img>>(item.Imgs);

            return await httpClientNew.SetJwt(jwt).Add(postNew);
        }

        public async Task<New> FindById(int id, string jwt = null)
        {
            return await httpClientNew.SetJwt(jwt).GetById(id);
        }

        public async Task<IEnumerable<New>> GetAll( string jwt = null)
        {
            return await httpClientNew.SetJwt(jwt).GetAll();
        }

        public async Task<New> GetByTitile(string title, string jwt = null)
        {
            return await httpClientNew.SetJwt(jwt).GetByTitle(title);
        }

        public async Task<int> Remove(string title, string jwt = null)
        {
            New remove_new = await httpClientNew.SetJwt(jwt).GetByTitle(title);

            return remove_new==null?404: await httpClientNew.SetJwt(jwt).Remove(remove_new.Id);
        }

        public async Task<int> Update(NewWrapperDto<PutNewDto> item, string jwt = null)
        {
            int result_op=0;
            New _new = await httpClientNew.SetJwt(jwt).GetByTitle(item.New.Title);
            var urlImgsForm = mapper.Map<List<Img>>(item.Imgs).Select(i => i.Url);
            var urlImgsDb = _new.Imgs.Select(i => i.Url);
            List<Img> removeImgs = _new.Imgs
                .Where(i => urlImgsDb.Except(urlImgsForm)
                .Contains(i.Url))
                .ToList();
            List<Img> addImgs = mapper.Map<List<Img>>(item.Imgs
                .Where(i => urlImgsForm.Except(urlImgsDb).Contains(i.Url)))
                .Select(i => new Img { Url = i.Url, NewId = _new.Id })
                .ToList();
            if (addImgs.Count != 0)
            {
              result_op=  await httpClientImg.SetJwt(jwt).AddRange(addImgs);
            }
            if (removeImgs.Count != 0 && result_op==200)
            {
                result_op= await httpClientImg.SetJwt(jwt).RemoveRange(removeImgs);
            }
            _new.Description = item.New.Description;

            return result_op==200? await httpClientNew.SetJwt(jwt).Update(_new): result_op;
        }
    }
}
