using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestWebApi.DataContext;

public abstract class TableBase
{
    /// <summary>
    ///     최초등록일시
    /// </summary>
    [Comment("최초등록일시")]
    public DateTime? createdAt { get; set; }

    /// <summary>
    ///     최초입력자
    /// </summary>
    [MaxLength(20)]
    [Comment("최초입력자")]
    public string? createdId { get; set; }

    /// <summary>
    ///     최초입력프로그램명
    /// </summary>
    [MaxLength(50)]
    [Comment("최초입력프로그램명")]
    public string? createdBy { get; set; }

    [MaxLength(50)] [Comment("최초입력ip주소")] public string? createdIp { get; set; }

    /// <summary>
    ///     최종수정일시
    /// </summary>
    [Comment("최종수정일시")]
    public DateTime? updatedAt { get; set; }

    /// <summary>
    ///     최종수정자
    /// </summary>
    [MaxLength(20)]
    [Comment("최종수정자")]
    public string? updatedId { get; set; }

    /// <summary>
    ///     최종수정프로그램명
    /// </summary>
    [MaxLength(50)]
    [Comment("최종수정프로그램명")]
    public string? updatedBy { get; set; }


    [MaxLength(50)] [Comment("최종입력ip주소")] public string? updatedIp { get; set; }

    /// <summary>
    ///     사용여부
    /// </summary>
    [Comment("사용여부")]
    [Required]
    public bool isValid { get; set; } = true;
}