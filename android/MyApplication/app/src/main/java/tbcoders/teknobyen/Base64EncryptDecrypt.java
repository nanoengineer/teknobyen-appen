package tbcoders.teknobyen;

import android.util.Base64;

/**
 * Created by Sølve on 16.05.2016.
 * http://stackoverflow.com/questions/30148729/how-to-secure-android-shared-preferences
 */
public class Base64EncryptDecrypt {
    public static String encrypt(String input) {
        // Simple encryption, not very strong!
        return Base64.encodeToString(input.getBytes(), Base64.DEFAULT);
    }

    public static String decrypt(String input) {
        return new String(Base64.decode(input, Base64.DEFAULT));
    }
}
