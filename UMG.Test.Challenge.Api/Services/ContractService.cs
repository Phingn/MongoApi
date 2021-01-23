using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Data;
using Movie.Api.Interface;
using Movie.Api.Models;
using Movie.Api.Models.dto;

namespace Movie.Api.Services
{
    public class ContractService : IContractService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ContractService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Artist> GetArtist()
        {
            return _context.Artists.ToList();
        }
        public List<DistributorAlbum> GetAlbums(string partnerName, DateTime StartDate, DateTime EndDate)
        {
            //return _context.DistributorAlbums
            //    .Where(x => x.PartnerName.ToString().ToLower() == partnerName.ToLower()
            //        && x.Contracts.StartDate >= StartDate && x.Contracts.EndDate <= EndDate)
            //    .ProjectTo<DistributorAlbum>(_mapper.ConfigurationProvider)
            //    .ToList();

            ////var DeliveryAlbums = _context.Artists
            ////                    .Join(_context.Albums, art => art.ArtistId, alb => alb.ArtistId,
            ////                        (art, alb) => new { alb, art })
            ////                    .Join(_context.Contracts, arts => arts.art.ArtistId, con => con.ArtistId,
            ////                        (arts, con) => new { con, arts })
            ////                    .Join(_context.DeliveryPartners, cons => cons.con.PartnerId, dep => dep.PartnerId,
            ////                        (cons, dep) => new { dep, cons })
            ////                      .Where(x => x.dep.Name.ToString().ToLower() == partnerName.ToLower()
            ////                            && x.cons.con.StartDate.Date <= StartDate.Date
            ////                            && (!x.cons.con.EndDate.HasValue  || x.cons.con.EndDate.Value.Date >= EndDate)
            ////                            )

            ////                        .Select(x => new DistributorAlbum
            ////                        {
            ////                            Contracts = new Contract
            ////                            {
            ////                                ContractId = x.cons.con.ContractId,
            ////                                ArtistId = x.cons.arts.art.ArtistId,
            ////                                PartnerId = x.cons.con.PartnerId,
            ////                                ContractType = x.cons.con.ContractType,
            ////                                StartDate = x.cons.con.StartDate,
            ////                                EndDate = x.cons.con.EndDate
            ////                            },
            ////                            PartnerName = x.dep.Name,
            ////                            ArtistName = x.cons.arts.art.Name,
            ////                            AlbumName = x.cons.arts.alb.Title
            ////                        }).ToList();

            ////return DeliveryAlbums;
            ///
            var distributionAlbum = new List<DistributorAlbum>();
            return distributionAlbum;
        }
    }
}
