package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.webkit.HttpAuthHandler;
import android.webkit.WebView;
import android.webkit.WebViewClient;

/**
 * Created by Sølve on 02.05.2016.
 */
public class MyWebViewClient extends WebViewClient {
    @Override
    public void onReceivedHttpAuthRequest(WebView view, HttpAuthHandler handler, String host, String realm){
        //Her hentes brukernavn og passord fra det som vart satt ved kjoering av MachineRefill eller MachineStatus ved autentisering
        String[] userData = view.getHttpAuthUsernamePassword("http://129.241.152.11/", "");
        handler.proceed(userData[0], userData[1]);
    }
}
