package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Base64;
import android.webkit.WebView;

import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import java.io.BufferedReader;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.ArrayList;
import java.util.List;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpression;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

public class MachineStatus extends AppCompatActivity {
    //This class is for opening the web browser inside the app.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if(true){
        setContentView(R.layout.activity_webview);
        oldWebView();
        }
        else{
        setContentView(R.layout.activity_washing_machine_status);


        try {
            urlScraping();
        } catch (IOException | ParserConfigurationException | XPathExpressionException | SAXException e) {
            e.printStackTrace();
        }

        }

    }

    private void urlScraping() throws IOException, ParserConfigurationException, XPathExpressionException, SAXException {
        /***
         *  "//div[contains(@class, 'reservation')]/table//td[contains(@class, 'p')]/text()"
         ***/
        SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");
        String httpURl = "http://129.241.152.11/";

        Retrievedata retrieveStatus = new Retrievedata();
        retrieveStatus.execute(name, password);


        //XPathExpression xpath = XPathFactory.newInstance().newXPath().compile("//td[text()=\"Description\"]/following-sibling::td[2]");

        //String result = (String) xpath.evaluate(NETTSIDEN, XPathConstants.STRING);

    }


    private void oldWebView() {
        //Oppretter nettleser og henter inn brukernavn og passord fra telefonen
        WebView webView = (WebView) findViewById(R.id.webView);
        webView.setWebViewClient(new MyWebViewClient());
        SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");
        webView.setHttpAuthUsernamePassword("http://129.241.152.11/", "", name, password);

        //Setup for webView
        webView.getSettings().setJavaScriptEnabled(true);
        webView.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        webView.getSettings().setDomStorageEnabled(true);
        webView.setVerticalScrollBarEnabled(false);
        webView.getSettings().setSupportMultipleWindows(true);
        webView.getSettings().setBuiltInZoomControls(true);
        webView.getSettings().setDisplayZoomControls(false);
        webView.setInitialScale(130);
        webView.loadUrl("http://129.241.152.11/LaundryState?lg=2&ly=9131");
    }
}

class Retrievedata extends AsyncTask<String, Void, String> {
    @Override
    protected String doInBackground(String... params) {
        try {
            URL url = new URL("http://129.241.152.11/LaundryState?lg=2&ly=9131");


            String userPassword = params[0] + ":" + params[1];
            String encoding = Base64.encodeToString(userPassword.getBytes(), Base64.DEFAULT);
            URLConnection uc = url.openConnection();
            uc.setRequestProperty("Authorization", "Basic " + encoding);
            uc.connect();
            InputStream inputStream = uc.getInputStream();

            BufferedReader in = new BufferedReader(new InputStreamReader(
                    inputStream));
            String inputLine;
            while ((inputLine = in.readLine()) != null)
                System.out.println(inputLine);
            in.close();

            File fileXml = new File("ada");
            DocumentBuilder parser = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            Document document = parser.parse(fileXml);

            XPathExpression xpath = XPathFactory.newInstance().newXPath().compile("//td[text()=\"Description\"]/following-sibling::td[2]");
            String result = (String) xpath.evaluate(document, XPathConstants.STRING);
            System.out.println("LALALALA");
            System.out.println(result);


        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (XPathExpressionException e) {
            e.printStackTrace();
        } catch (ParserConfigurationException e) {
            e.printStackTrace();
        } catch (SAXException e) {
            e.printStackTrace();
        }
        return null;
    }
}

