using AutoMapper;
using AutoMapper.Internal.Mappers;
using StudentApp.Model;
using StudentApp.Domain;
using StudentApp.DTO;

namespace StudentApp.Extensions;

public static class MappingExtensions
{
    #region ToModel
    
    public static StudentEntity ToModel(this Student student)
    {
        return Mapper<Student, StudentEntity>(student, cfg =>
        {
            cfg.CreateMap<Student, StudentEntity>();
            cfg.CreateMap<Weekend, WeekendEntity>();
        });
    }
    
    public static WeekendEntity ToModel(this Weekend weekend)
    {
        return Mapper<Weekend, WeekendEntity>(weekend, cfg =>
        {
            cfg.CreateMap<Weekend, WeekendEntity>();
        });
    }
    
    public static CourseEntity ToModel(this Course course)
    {
        return Mapper<Course, CourseEntity>(course, cfg =>
        {
            cfg.CreateMap<Course, CourseEntity>();
        });
    }
    
    public static StudentCourseEntity ToModel(this StudentCourse studentCourse)
    {
        return Mapper<StudentCourse, StudentCourseEntity>(studentCourse, cfg =>
        {
            cfg.CreateMap<StudentCourse, StudentCourseEntity>()
                .ForMember(dest => dest.CourseId,
                    opt => opt.MapFrom(src => src.Course.Id))
                .ForMember(dest => dest.StudentId,
                    opt => opt.MapFrom(src => src.Student.Id))
                .ForMember(dest => dest.Course,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Student,
                    opt => opt.Ignore());
        });
    }
    
    #endregion
    
    #region ToDomain

    public static Student ToDomain(this StudentEntity studentEntity)
    {
        return Mapper<StudentEntity, Student>(studentEntity, cfg =>
        {
            cfg.CreateMap<StudentCourseEntity, StudentCourse>();
            cfg.CreateMap<StudentEntity, Student>();
            cfg.CreateMap<CourseEntity, Course>();
            cfg.CreateMap<WeekendEntity, Weekend>();
        });
    }
    
    public static Weekend ToDomain(this WeekendEntity weekendEntity)
    {
        return Mapper<WeekendEntity, Weekend>(weekendEntity, cfg =>
        {
            cfg.CreateMap<WeekendEntity, Weekend>();
        });
    }
    
    public static Course ToDomain(this CourseEntity courseEntity)
    {
        return Mapper<CourseEntity, Course>(courseEntity, cfg =>
        {
            cfg.CreateMap<CourseEntity, Course>();
        });
    }

    public static StudentCourse ToDomain(this CourseAttachRequestDto courseAttachRequestDto)
    {
        return Mapper<CourseAttachRequestDto, StudentCourse>(courseAttachRequestDto, cfg =>
        {
            cfg.CreateMap<CourseAttachRequestDto, StudentCourse>()
                .AfterMap((src, dest) =>
                {
                    dest.Course = new Course
                    {
                        Id = src.CourseId
                    };
                })
                .AfterMap((src, dest) => dest.Student = new Student()
                {
                    Id = src.StudentId
                });
                // .ForMember(dest => dest.Course.Id,
                //     opt => opt.MapFrom(src => src.CourseId))
                // .ForMember(dest => dest.Student.Id,
                //     opt => opt.MapFrom(src => src.StudentId));
        });
    }

    public static Weekend ToDomain(this WeekendAttachRequestDto weekendAttachRequestDto)
    {
        return Mapper<WeekendAttachRequestDto, Weekend>(weekendAttachRequestDto, cfg =>
        {
            cfg.CreateMap<WeekendAttachRequestDto, Weekend>();
        });
    }

    public static Student ToDomain(this StudentCreateRequestDto studentCreateRequestDto)
    {
        return Mapper<StudentCreateRequestDto, Student>(studentCreateRequestDto, cfg =>
        {
            cfg.CreateMap<StudentCreateRequestDto, Student>();
        });
    }

    public static Course ToDomain(this CourseCreateRequestDto courseCreateRequestDto)
    {
        return Mapper<CourseCreateRequestDto, Course>(courseCreateRequestDto, cfg =>
        {
            cfg.CreateMap<CourseCreateRequestDto, Course>();
        });
    }
    
    #endregion
    
    private static TDestination Mapper<TSource, TDestination>(
        this TSource source,
        Action<IMapperConfigurationExpression> configure)
    {
        var config = new MapperConfiguration(configure);
        var mapper = config.CreateMapper();
        var destination = mapper.Map<TDestination>(source);
        return destination;
    }
}