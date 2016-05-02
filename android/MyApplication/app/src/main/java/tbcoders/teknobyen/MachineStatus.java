package tbcoders.teknobyen;

import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.webkit.WebView;

public class MachineStatus extends AppCompatActivity {
    //This class is for opening the web browser inside the app.
    //But it doesn't work yet because I get an access denied error when trying to open the webpage.

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_webView);
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
        webView.getSettings().setBuiltInZoomControls(true);
        webView.getSettings().setDisplayZoomControls(false);
        webView.setInitialScale(130);
        webView.loadUrl("http://129.241.152.11/LaundryState?lg=2&ly=9131");
    }
}
