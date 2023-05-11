package DAO;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;

public class RestfulApiConnector {
    public RestfulApiConnector()
    {

    }

    public String RetrieveStringFromServerUsingGet(String urlString, String...headers) {
        String jsonString = "";

        try {
            URL url = new URL(urlString);

            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");

            for(int i = 0; i < headers.length / 2; ++i) {
                conn.setRequestProperty(headers[i * 2], headers[i * 2 + 1]);
            }

            // Send the GET request
            int responseCode = conn.getResponseCode();

            if (responseCode == HttpURLConnection.HTTP_OK) {

                BufferedReader reader = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                String line;
                StringBuilder response = new StringBuilder();

                while ((line = reader.readLine()) != null) {
                    response.append(line);
                }

                reader.close();

                jsonString = response.toString();
            }
            else {
                System.out.println("GET request failed. Response Code: " + responseCode);
            }
        }
        catch(Exception e) {
            System.out.println(e.toString());
        }

        return jsonString;
    }

    public String RetrieveStringFromKakao(String query) throws Exception {
        final String url = "https://dapi.kakao.com/v2/search/image?query=";
        final String encodedQuery = URLEncoder.encode(query, "UTF-8");

        final String authorizationName = "Authorization";
        final String authorizationKey = "KakaoAK c0c5d8b5d9a744f773681e4f51beba24";

        return RetrieveStringFromServerUsingGet(url + encodedQuery, authorizationName, authorizationKey);
    }

    public String RetrieveRandomWords() {
        final String url = "https://random-word-api.vercel.app/api?words=10";

        return RetrieveStringFromServerUsingGet(url);
    }
}
