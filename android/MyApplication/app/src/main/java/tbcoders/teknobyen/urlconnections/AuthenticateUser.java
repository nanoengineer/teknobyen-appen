package tbcoders.teknobyen.urlconnections;

import android.os.AsyncTask;
import android.util.Base64;

import org.jsoup.Jsoup;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.ProtocolException;
import java.net.URL;

import tbcoders.teknobyen.Base64EncryptDecrypt;

/**
 * Created by Alexander on 15/05/2016.
 */
public class AuthenticateUser extends AsyncTask<String, Void, String> {
    @Override
    protected String doInBackground(String... params) {
        try {
            URL url = null;
            url = new URL("http://129.241.152.11/LaundryState?lg=2&ly=9131");
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            String userPassword = params[0] + ":" + params[1];
            String basicAuth = "Basic " + Base64.encodeToString(userPassword.getBytes(), Base64.DEFAULT);
            connection.setRequestProperty("Authorization", basicAuth);
            connection.setRequestMethod("GET");
            connection.connect();

            int code = connection.getResponseCode();
            return String.valueOf(code);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}