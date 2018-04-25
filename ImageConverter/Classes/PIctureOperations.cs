using System;
using UniversalImageConverter.Structure;

namespace UniversalImageConverter.Classes
{
    public class PictureOperations
    {
        private Database _database;

        public PictureOperations()
        {
            _database = new Database();
        }

        public void UploadPictureToDB(ObjectExtraData data)
        {
            try
            {
                var query = "WITH pictures AS ( "
                               + "UPDATE pictures SET photo = @photo "
                               + "WHERE article_id = @article_id "
                               + "RETURNING *) "
                               + "INSERT INTO pictures(article_id, photo) "
                               + "SELECT @article_id, @photo "
                               + "WHERE NOT EXISTS(SELECT * FROM photos)";

                _database.InsertImage(query, data);
            }

            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {DateTime.UtcNow} - UploadPictureToDB | {e.Message}");
            }
        }

        internal byte[] GetPicture(int article_id)
        {
            byte[] picture = null;

            try
            {
                var query = $"SELECT photo FROM pictures WHERE article_id = {article_id}";
                picture = _database.GetImage(query);
            }

            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {DateTime.UtcNow} - GetPicture | {e.Message}");
            }

            return picture;
        }
    }
}
