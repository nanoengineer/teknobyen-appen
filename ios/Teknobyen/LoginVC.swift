//
//  LoginVC.swift
//  Teknobyen
//
//  Created by Tony Wu on 2016-05-07.
//  Copyright © 2016 Mathias Breistein. All rights reserved.
//

import UIKit

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

        self.loginScrollView.bounces = true
        self.loginScrollView.alwaysBounceVertical = true
        self.loginScrollView.delegate = self
        self.navigationItem.title = "Logg Inn"
        
        
        
        textFieldDelegatesSet()
        
        let tap: UITapGestureRecognizer = UITapGestureRecognizer(target: self, action: #selector(LoginVC.dismissKeyboard))
        view.addGestureRecognizer(tap)
        
        self.loginButton.tintColor = AppConstants.themeBlueColor
        self.loginButton.layer.cornerRadius = 5
        
        self.loginVerificationIndicatorUpdate()

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
        
    }

    private func loginVerificationIndicatorUpdate() {
        switch self.verificationState {
        case .notVerified:
            self.loginVerifyImage.image = UIImage(named: "Cancel")
        case .verified:
            self.loginVerifyImage.image = UIImage(named: "Ok")
        case .unknown:
            self.loginVerifyImage.image = UIImage(named: "Question Mark")
        }
    }

    @IBAction func loginButtonPressed(sender: UIButton) {
    
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
            let contentInsets = UIEdgeInsets(top: 0.0, left: 0.0, bottom: keyboardSize.height + 40, right: 0.0)
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
            self.loginVerifcationRequest()            
        }
        return true
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
