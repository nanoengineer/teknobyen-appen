package tbcoders.teknobyen.urlconnections;

import android.os.AsyncTask;
import android.util.Base64;

import org.jsoup.Jsoup;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Arrays;

import tbcoders.teknobyen.Base64EncryptDecrypt;

/**
 * Created by Alexander on 15/05/2016.
 */
public class RetreiveWashingMachineBalance extends AsyncTask<String, Void, String> {
    @Override
    protected String doInBackground(String... params) {

        String userPassword = Base64EncryptDecrypt.decrypt(params[0]) + ":" + Base64EncryptDecrypt.decrypt(params[1]);
        String encoding = Base64.encodeToString(userPassword.getBytes(), Base64.DEFAULT);

        org.jsoup.nodes.Document document = null;
        try {
            document = Jsoup
                    .connect("http://129.241.152.11/SaldoForm?lg=2&ly=9131")
                    .header("Authorization", "Basic " + encoding)
                    .get();

            Elements data = document.getElementsByClass("p");
            Integer balanseIndex = null;
            String balance = "Balance: ";
            for (int i = 0; i < data.size(); i++) {
                String s = data.get(i).toString();
                s = s.substring(s.indexOf(">")+1, s.lastIndexOf("</td>"));
                if(s.equals("BALANSE")){
                    balanseIndex = i;
                }
                if(balanseIndex != null && i == balanseIndex + 1){
                    balance += s;
                }
            }
            return balance;


        } catch (IOException e) {
            System.out.println("MachineStatusActivity IOException");
            e.printStackTrace();
            return null;
        }
    }
}