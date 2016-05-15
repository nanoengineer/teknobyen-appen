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
        
        // Do any additional setup after loading the view.
    }

    deinit {
        NSNotificationCenter.defaultCenter().removeObserver(self)
    }

    func adjustInsetForKeyboardShow(show: Bool, notification: NSNotification) {
        guard let value = notification.userInfo?[UIKeyboardFrameBeginUserInfoKey] as? NSValue else { return }
        let keyboardFrame = value.CGRectValue()
        let adjustmentHeight = (CGRectGetHeight(keyboardFrame) + 20) * (show ? 1 : -1)
        loginScrollView.contentInset.bottom += adjustmentHeight
        loginScrollView.scrollIndicatorInsets.bottom += adjustmentHeight
    }

    func keyboardWillShow(notification: NSNotification) {
        adjustInsetForKeyboardShow(true, notification: notification)
    }

    func keyboardWillHide(notification: NSNotification) {
        adjustInsetForKeyboardShow(false, notification: notification)
    }
    
    func textFieldShouldReturn(textField: UITextField) -> Bool {
        if textField == nameTextField {
            roomNumTextField.becomeFirstResponder()
        } else if textField == roomNumTextField {
            usernameTextField.becomeFirstResponder()
        } else if usernameTextField.becomeFirstResponder() {
            pwdTextField.becomeFirstResponder()
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
