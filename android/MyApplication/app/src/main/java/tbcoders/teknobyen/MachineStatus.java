package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.net.http.SslError;
import android.os.Build;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.webkit.SslErrorHandler;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

public class MachineStatus extends AppCompatActivity {
    //This class is for opening the web browser inside the app.
    //But it doesn't work yet because I get an access denied error when trying to open the webpage.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_machine_status);
        SharedPreferences sharedPref = getSharedPreferences("mypref", 0);
        String name = sharedPref.getString("username", "");
        String password = sharedPref.getString("password", "");


        WebView webView = (WebView)findViewById(R.id.webView);
        webView.setWebViewClient(new MyWebViewClient());
        webView.getSettings().setSupportMultipleWindows(true);
        webView.setHttpAuthUsernamePassword("http://129.241.152.11/", "", name, password);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        webView.getSettings().setDomStorageEnabled(true);
        webView.setVerticalScrollBarEnabled(false);
        webView.loadUrl("http://129.241.152.11/LaundryState?lg=2&ly=9131");
    }
}