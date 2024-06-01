namespace RST.Configurations
{
    public class AddImage
    {
        public static async Task<string> AddImageAsync(IFormFile image, IWebHostEnvironment hostingEnvironment)
        {
            // Check if an image file was uploaded
            if (image != null && image.Length > 0)
            {
                // Generate a unique file name for the image
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;

                // Save the image file to a specified location (e.g., wwwroot/images/)
                string imagePath = Path.Combine(hostingEnvironment.WebRootPath, "Images", uniqueFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // return the image name
                return uniqueFileName;
            }
            return "";
        }
    }
}
