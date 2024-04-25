using AutoMapper;
using CodeShellCore.Files;

namespace CodeShellCore.FileServer
{
    public class CodeshellFileServerAutoMapperProfile : Profile
    {
        public CodeshellFileServerAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<AttachmentCategory, AttachmentCategoryDto>();
            CreateMap<Dimension, DimensionDto>();
            CreateMap<UploadedFileInfo, UploadedFileInfoDto>();
            CreateMap<ITempFileData, SaveAttachmentRequestDto>();
        }
    }
}