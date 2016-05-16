//
//  LoginVC.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-07.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit
import Locksmith


class LoginVC: UIViewController, UITextFieldDelegate, UIScrollViewDelegate {

    @IBOutlet weak var loginButton: UIButton!
    @IBOutlet weak var loginVerifyImage: UIImageView!
    @IBOutlet weak var usernameTextField: UITextField!
    @IBOutlet weak var pwdTextField: UITextField!
    @IBOutlet weak var nameTextField: UITextField!
    @IBOutlet weak var roomNumTextField: UITextField!
    @IBOutlet weak var loginScrollView: UIScrollView!
    
    weak var activeTextField: UITextField?
    
    enum washingMachineVerificationState {
        case notVerified
        case verified
        case unknown
    }
    
    private var verificationState: washingMachineVerificationState = .unknown
    
    override func viewDidLoad() {
        super.viewDidLoad()

        self.uiSetUp()
        self.keyboardSetUp()

    }
    
    private func keyboardSetUp() {
        NSNotificationCenter.defaultCenter().addObserver(
            self,
            selector: #selector(LoginVC.keyboardWillShow(_:)),
            name: UIKeyboardWillShowNotification,
            object: nil
        )
        
        NSNotificationCenter.defaultCenter().addObserver(
            self,
            selector: #selector(LoginVC.keyboardWillHide(_:)),
            name: UIKeyboardWillHideNotification,
            object: nil
        )
        
        let tap: UITapGestureRecognizer = UITapGestureRecognizer(target: self, action: #selector(LoginVC.dismissKeyboard))
        view.addGestureRecognizer(tap)
    }
    
    private func uiSetUp() {
        self.loginScrollView.bounces = true
        self.loginScrollView.alwaysBounceVertical = true
        self.loginScrollView.delegate = self
        
        self.navigationItem.title = "Logg inn"
        
        self.loginButton.backgroundColor = AppConstants.themeGreenColor
        self.loginButton.setTitle(self.navigationItem.title, forState: UIControlState.Normal)
        self.loginButton.layer.cornerRadius = 5
        self.loginButton.titleLabel?.font = UIFont(name: AppConstants.normalFontName, size: 22)
        
        self.loginVerificationIndicatorUpdate()
        
        textFieldDelegatesSet()
        
    }

    private func loginVerificationIndicatorUpdate() {
        switch self.verificationState {
        case .notVerified:
            self.loginVerifyImage.image = UIImage(named: "Cancel")
            self.loginButton.backgroundColor = UIColor.grayColor().colorWithAlphaComponent(0.5)
            self.loginButton.enabled = false
        case .verified:
            self.loginVerifyImage.image = UIImage(named: "Ok")
            self.loginButton.backgroundColor = AppConstants.themeGreenColor
            self.loginButton.enabled = true
        case .unknown:
            self.loginVerifyImage.image = UIImage(named: "Question Mark")
            self.loginButton.backgroundColor = UIColor.grayColor().colorWithAlphaComponent(0.5)
            self.loginButton.enabled = false
        }
    }

    @IBAction func loginButtonPressed(sender: UIButton) {
        
        self.loginVerifcationRequest()
        
        if verificationState == .verified {
            UserTBCredentials.name = nameTextField.text!
            UserTBCredentials.roomNumber = roomNumTextField.text!
            UserTBCredentials.username = usernameTextField.text!
            UserTBCredentials.password = pwdTextField.text!
            
            do {
                try UserTBCredentials.createInSecureStore()
            }
            catch {
                print("Storage Error")
            }
        }
        else {
            print("Invalid Info")
        }
    }
    @IBAction func deleteCredentialsPressed(sender: UIButton) {
        
        do {
            try UserTBCredentials.deleteFromSecureStore()
        }
        catch {
            print("Deletion Error")
        }
    }

    private func loginVerifcationRequest() {
        let username = self.usernameTextField.text!
        let pwd = self.pwdTextField.text!
        
        let config = NSURLSessionConfiguration.defaultSessionConfiguration()
        let userPasswordString = "\(username):\(pwd)"
        let userPasswordData = userPasswordString.dataUsingEncoding(NSUTF8StringEncoding)
        let base64EncodedCredential = userPasswordData!.base64EncodedStringWithOptions(NSDataBase64EncodingOptions.Encoding64CharacterLineLength)
        let authString = "Basic \(base64EncodedCredential)"
        config.HTTPAdditionalHeaders = ["Authorization" : authString]
        let session = NSURLSession(configuration: config)
        
        let url = NSURL(string: "http://129.241.152.11/MineReservationer")
        let task = session.dataTaskWithURL(url!) {
            (let data, let response, let error) in
            
            if let httpResponse = response as? NSHTTPURLResponse
            {
                
                if httpResponse.statusCode == 200
                {
                    self.verificationState = .verified
                }
                else {
                    print("Reponse: \(httpResponse.statusCode)")
                    self.verificationState = .notVerified
                }
            }
            dispatch_async(dispatch_get_main_queue(),{
                self.loginVerificationIndicatorUpdate()
            })
            
        }
        task.resume()
    }

    func keyboardWillShow(notification: NSNotification) {
        if let activeField = self.activeTextField, keyboardSize = (notification.userInfo?[UIKeyboardFrameBeginUserInfoKey] as? NSValue)?.CGRectValue() {
            let contentInsets = UIEdgeInsets(top: 0.0, left: 0.0, bottom: keyboardSize.height, right: 0.0)
            self.loginScrollView.contentInset = contentInsets
            self.loginScrollView.scrollIndicatorInsets = contentInsets
            var aRect = self.view.frame
            aRect.size.height -= keyboardSize.size.height
            if (!CGRectContainsPoint(aRect, activeField.frame.origin)) {
                self.loginScrollView.scrollRectToVisible(activeField.frame, animated: true)
            }
        }
    }

    func keyboardWillHide(notification: NSNotification) {
        let contentInsets = UIEdgeInsetsZero
        self.loginScrollView.contentInset = contentInsets
        self.loginScrollView.scrollIndicatorInsets = contentInsets
    }
    
    func textFieldDidBeginEditing(textField: UITextField) {
        self.activeTextField = textField
    }
    
    func textFieldDidEndEditing(textField: UITextField) {
        self.activeTextField =  nil
        if textField == usernameTextField || textField == pwdTextField {
            if textField == usernameTextField {
                if pwdTextField.text != "" {
                    self.loginVerifcationRequest()
                }
            }
            else {
                self.loginVerifcationRequest()
            }
        }
    }
    
    func textFieldShouldReturn(textField: UITextField) -> Bool {
        if textField == nameTextField {
            roomNumTextField.becomeFirstResponder()
        } else if textField == roomNumTextField {
            usernameTextField.becomeFirstResponder()
        } else if textField == usernameTextField {
            pwdTextField.becomeFirstResponder()
        } else if textField == pwdTextField {
            pwdTextField.resignFirstResponder()
        }
        return true
    }
    
    func textField(textField: UITextField, shouldChangeCharactersInRange range: NSRange, replacementString string: String) -> Bool {
        if textField == roomNumTextField {
            let currentCharacterCount = textField.text?.characters.count ?? 0
            if (range.length + range.location > currentCharacterCount){
                return false
            }
            let newLength = currentCharacterCount + string.characters.count - range.length
            return newLength <= 3
        }
        else {
            return true
        }
    }
    
    private func textFieldDelegatesSet() {
        usernameTextField.delegate = self
        pwdTextField.delegate = self
        nameTextField.delegate = self
        roomNumTextField.delegate = self
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }
    
    func dismissKeyboard() {
        //Causes the view (or one of its embedded text fields) to resign the first responder status.
        view.endEditing(true)
    }



    /*
    // MARK: - Navigation

    // In a storyboard-based application, you will often want to do a little preparation before navigation
    override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
        // Get the new view controller using segue.destinationViewController.
        // Pass the selected object to the new view controller.
    }
    */

}
