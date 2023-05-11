package DAO;

import DTO.ImageInformationDTO;
import org.json.JSONArray;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Arrays;

public class ImageInformationDAO {
    private static ImageInformationDAO _instance;
    private RestfulApiConnector restfulApiConnector;

    private ImageInformationDAO()
    {
        this.restfulApiConnector = new RestfulApiConnector();
    }

    public static ImageInformationDAO GetInstance()
    {
        if(_instance == null)
        {
            _instance = new ImageInformationDAO();
        }

        return _instance;
    }

    public void Test() throws Exception
    {

        ArrayList<String> strings = StringToWordList(this.restfulApiConnector.RetrieveRandomWords());


        ArrayList<ImageInformationDTO> test = JsonToImageInformationDtoList(this.restfulApiConnector.RetrieveStringFromKakao("apple"));

        for(ImageInformationDTO temp:test)
        {
            System.out.println(temp.GetThumbnailUrl());
        }
    }

    public ArrayList<ImageInformationDTO> JsonToImageInformationDtoList(String jsonString)
    {
        ArrayList<ImageInformationDTO> imageInformationList = new ArrayList<ImageInformationDTO>();

        JSONObject jsonObject = new JSONObject(jsonString);
        JSONArray jsonArray = jsonObject.getJSONArray("documents");

        for (int i = 0; i < jsonArray.length(); i++)
        {
            int width = jsonArray.getJSONObject(i).getInt("width");
            int height = jsonArray.getJSONObject(i).getInt("height");
            String imageUrl = jsonArray.getJSONObject(i).getString("image_url");
            String thumbnailUrl = jsonArray.getJSONObject(i).getString("thumbnail_url");

            imageInformationList.add(new ImageInformationDTO(width, height, imageUrl, thumbnailUrl));
        }

        return imageInformationList;
    }

    public ArrayList<String> StringToWordList(String str)
    {
        ArrayList<String> randomWordList = new ArrayList<String>();

        String convertedString = str.substring(1, str.length() - 1);
        String []strList = convertedString.split(",");

        for(int i = 0; i < strList.length; ++i)
        {
            strList[i] = strList[i].substring(1, strList[i].length() - 1);
;       }

        randomWordList.addAll(Arrays.asList(strList));

        return randomWordList;
    }
}
