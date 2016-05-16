package tbcoders.teknobyen;

import android.os.AsyncTask;
import android.util.Base64;

import org.jsoup.Jsoup;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Created by Alexander on 15/05/2016.
 */
class RetreiveWashingMachineStatus extends AsyncTask<String, Void, String> {
    @Override
    protected String doInBackground(String... params) {
        try {
            URL statusUrl = new URL("http://129.241.152.11/LaundryState?lg=2&ly=9131");
        } catch (MalformedURLException e) {
            System.out.println("MachineStatusActivity MalformedURLException");
            e.printStackTrace();
        }

        String userPassword = Base64EncryptDecrypt.decrypt(params[0]) + ":" + Base64EncryptDecrypt.decrypt(params[1]);
        String encoding = Base64.encodeToString(userPassword.getBytes(), Base64.DEFAULT);

        org.jsoup.nodes.Document document = null;
        try {
            document = Jsoup
                    .connect("http://129.241.152.11/LaundryState?lg=2&ly=9131")
                    .header("Authorization", "Basic " + encoding)
                    .get();

            Elements data = document.getElementsByClass("p");

            String status = "";
            for (int i = 0; i < data.size(); i++) {
                status += data.get(i).toString().split("<br>")[1] + ",";
            }
            return status;

        } catch (IOException e) {
            System.out.println("MachineStatusActivity IOException");
            e.printStackTrace();
            return null;
        }
    }
}