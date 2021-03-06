using System;
using DockerMgr.DTO;
using DockerMgr.DTO.ImageDTO;
using DockerMgr.Services;
using DockerMgr.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DockerMgr.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("all_by_ip")]
        public ActionResult<Msg> GetAllByIp([FromBody] GetImageByIpDTO getImageByIpDto)
        {
            var ip = getImageByIpDto.Ip;
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.GetAllImagesByIp(ip)
            };
            return r;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("all_by_id")]
        public ActionResult<Msg> GetAllById([FromBody] GetImageByIdDTO getImageByIdDto)
        {
            var id = getImageByIdDto.Id;
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.GetAllImagesByUserId(id)
            };
            return r;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("search")]
        public ActionResult<Msg> Search([FromBody] SearchImageDTO searchImageDto)
        {
            var ip = searchImageDto.Ip;
            var keyWord = searchImageDto.KeyWord;
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.SearchImages(keyWord, ip)
            };
            return r;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("inspect")]
        public ActionResult<Msg> Inspect([FromBody] InspectImageDTO inspectImageDto)
        {
            var ip = inspectImageDto.Ip;
            var name = inspectImageDto.name;
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.ImageDetail(ip, name)
            };
            return r;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("delete")]
        public ActionResult<Msg> Delete([FromBody] DeleteImageDTO deleteImageDto)
        {
            var ip = deleteImageDto.Ip;
            var id = deleteImageDto.Id;
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.DeleteImage(ip, id)
            };
            return r;
        }
        
        [AllowAnonymous]
        [HttpPost, Route("pull")]
        public ActionResult<Msg> PullImage([FromBody] PullImageDTO pullImageDto)
        {
            var ip = pullImageDto.Ip;
            var name = pullImageDto.Name;
            var tag = pullImageDto.Tag;
            var guid = Guid.NewGuid().ToString();
            
            try
            {
                _imageService.PullImage(ip, name, tag, guid);
            }
            catch (Exception e)
            {
                var er = new Msg{
                    Message = MsgCode.RES_ERROR,
                    Data = e
                };

                return er;
            }
            
            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = guid
            };
            return r;
        }
        
        
        [AllowAnonymous]
        [HttpPost, Route("query")]
        public ActionResult<Msg> queryImage([FromBody] QueryImageDTO queryImageDto)
        {
            var guid = queryImageDto.Guid;

            var r = new Msg{
                Message = MsgCode.SUCCESS,
                Data = _imageService.PullDetail(guid)
            };
            return r;
        }
    }
}