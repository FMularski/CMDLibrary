// <auto-generated />
namespace CMDLibrary.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class CreatedGenreTableAndAddedGenreIdColumnToBooksTable : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(CreatedGenreTableAndAddedGenreIdColumnToBooksTable));
        
        string IMigrationMetadata.Id
        {
            get { return "201806242100234_CreatedGenreTableAndAddedGenreIdColumnToBooksTable"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
