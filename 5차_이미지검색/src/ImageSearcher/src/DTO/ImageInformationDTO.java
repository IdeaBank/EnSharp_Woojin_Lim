package DTO;

public class ImageInformationDTO {
    private int width;
    private int height;
    private String imageUrl;
    private String thumbnailUrl;

    public int GetWidth()
    {
        return this.width;
    }

    public int GetHeight()
    {
        return this.height;
    }

    public String GetImageUrl()
    {
        return this.imageUrl;
    }

    public String GetThumbnailUrl()
    {
        return this.thumbnailUrl;
    }

    public ImageInformationDTO(int width, int height, String imageUrl, String thumbnailUrl)
    {
        this.width = width;
        this.height = height;
        this.imageUrl = imageUrl;
        this.thumbnailUrl = thumbnailUrl;
    }
}
