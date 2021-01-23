using System;
using Xunit;
using Movie.Api.Models;
using System.IO;
using System.Linq;
using Movie.Api.Helper;
using System.Collections.Generic;
using Movie.Api.Models.dto;
using Movie.Api.Data;
using Moq;
using Movie.Api.Services;
using AutoMapper;
using Movie.Api.Data.Configuration;
using Movie.Api.Interface;
using Movie.Api.Tests.Data;
using Microsoft.EntityFrameworkCore;

namespace Movie.Api.Tests
{
    public class TestActions
    {
        private readonly Mock<DataContext> _mockDataContext;
        private readonly Mock<ContractService> _mockContractService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly DataContext _context;

        private IContractService _contractService;
        

        private Mapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));

            return new Mapper(configuration);
        }

        public TestActions()
        {
            _mockDataContext = new Mock<DataContext>();
            _mockContractService = new Mock<ContractService>();
            _mockMapper = new Mock<IMapper>();

            // using EntityFrameworkCoreInMemory to mock datacontext object
            _context = DBContext.DataSourceMemoryContext("db");

            _contractService = new ContractService(_context, _mockMapper.Object);
        }
        [Fact]
        public void Test1()
        {
            string subdirectory = $@"Data\";
            string parentPath = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName}\{subdirectory}";

            string fileName = "Artist.csv";
            List<Artist> Artists = File.ReadAllLines($"{parentPath}\\{fileName}")
                            .Skip(1)
                            .Select(x => x.Split(','))
                            .Select(x => new Artist
                            {
                                ArtistId = x[0],
                                Name = x[1]
                            }).ToList();


            _context.Artists.AddRange(Artists);
            _context.SaveChanges();

            var artists = _context.Artists.ToList();


            fileName = "Contract.csv";
           var Contracts = File.ReadAllLines($"{parentPath}\\{fileName}")
                            .Skip(1)
                            .Select(x => x.Split(','))
                            .Select(x => new Contract
                            {
                                ContractId = int.Parse(x[0]),
                                ArtistId = int.Parse(x[1]),
                                PartnerId = int.Parse(x[2]),
                                StartDate = x[3] != string.Empty ? DateTime.Parse(x[3]) : DateTime.MinValue,
                                EndDate = x[4] != string.Empty ? DateTime.Parse(x[4]) : DateTime.MaxValue,
                                ContractType = (ContractTypeEnum) Enum.Parse(typeof(ContractTypeEnum),x[5])
                            }).ToList();

            _context.Contracts.AddRange(Contracts);
            _context.SaveChanges();

            fileName = "Album.csv";
            var Albums = File.ReadAllLines($"{parentPath}\\{fileName}")
                            .Skip(1)
                            .Select(x => x.Split(','))
                            .Select(x => new Album
                            {
                                AlbumId = int.Parse(x[0]),
                                ArtistId = int.Parse(x[1]),
                                Title = x[2]
                            }).ToList();

            _context.Albums.AddRange(Albums);
            _context.SaveChanges();

            fileName = "DeliveryPartner.csv";
            var DeliveryPartners = File.ReadAllLines($"{parentPath}\\{fileName}")
                .Skip(1)
                .Select(x => x.Split(','))
                .Select(x => new DeliveryPartner
                {
                    PartnerId = int.Parse(x[0]),
                    Name = x[1]
                }).ToList();

            _context.DeliveryPartners.AddRange(DeliveryPartners);
            _context.SaveChanges();

            //Query List using inner join
            //var DeliveryAlbums = Artists
            //                    .Join(Albums, art => art.ArtistId, alb => alb.ArtistId,
            //                        (art, alb) => new { alb, art })
            //                    .Join(Contracts, arts => arts.art.ArtistId, con => con.ArtistId,
            //                        (arts, con) => new { con, arts })
            //                    .Join(DeliveryPartners, cons => cons.con.PartnerId, dep => dep.PartnerId,
            //                        (cons, dep) => new { dep, cons })
            //                        .Select(x => new DistributorAlbum
            //                        {
            //                            Contracts = new Contract {
            //                                            ContractId = x.cons.con.ContractId,
            //                                            ArtistId = x.cons.arts.art.ArtistId,
            //                                            PartnerId = x.cons.con.PartnerId,
            //                                            ContractType = x.cons.con.ContractType,
            //                                            StartDate = x.cons.con.StartDate,
            //                                            EndDate = x.cons.con.EndDate

            //                            },
            //                            PartnerName = x.dep.Name,
            //                            ArtistName = x.cons.arts.art.Name,
            //                            AlbumName = x.cons.arts.alb.Title
            //                        }).ToList();

            List<DistributorAlbum> DeliveryAlbums = new List<DistributorAlbum>();
            var partnerName = "YouTube";
            DateTime startDate =  DateTime.Parse("01 mar 2012");
            DateTime endDate = DateTime.Parse("30 jan 2021");

            List<DistributorAlbum> distributorAlbums = new List<DistributorAlbum>();
            distributorAlbums = (List<DistributorAlbum>) DeliveryAlbums;

            //_mockContractService
            //    .Setup(m => m.GetAlbums(partnerName, startDate, endDate))
            //    .Returns(distributorAlbums);

            var contractAlbums = _contractService.GetAlbums(partnerName, startDate, endDate);

            Assert.NotNull(contractAlbums);
            //Arrange
            //Action
            //Assert

            partnerName = "iTunes";
            startDate = DateTime.Parse("01 mar 2012");
            endDate = DateTime.Parse("30 jan 2021");

            contractAlbums = _contractService.GetAlbums(partnerName, startDate, endDate);

            Assert.NotNull(Artists);
        }
    }
}
