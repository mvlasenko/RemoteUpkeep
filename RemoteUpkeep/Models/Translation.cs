using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RemoteUpkeep.Models
{
    public class Translation
    {
        [Key]
        public int Id { get; set; }

        public int RecordId { get; set; }

        public Table Table { get; set; }

        public Field Field { get; set; }

        [UIHint("_Language")]
        [Display(Name = "Language", ResourceType = typeof(Properties.Resources))]
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Translation", ResourceType = typeof(Properties.Resources))]
        public string TranslationValue { get; set; }

        [NotMapped]
        [ReadOnly(true)]
        [UIHint("MultilineText")]
        [Display(Name = "Original", ResourceType = typeof(Properties.Resources))]
        public string OriginalValue { get; set; }
    }
}