# Generate a Self Signed SSL Certificate

#### Authors: Rafiqul Islam Robin 

## What is a certificate authority (CA)?

* A certificate authority (CA) is a trusted entity that issues Secure Sockets Layer (SSL) certificates. These digital certificates are data files used to cryptographically link an entity with a public key. Web browsers use them to authenticate content sent from web servers, ensuring trust in content delivered online.
* As providers of these certificates, CAs are a reliable and critical trust anchor of the Internet's public key infrastructure (PKI). They help secure the internet for both organizations and users.
* The main goal of a CA is to verify the authenticity and trustworthiness of a website, domain and organization so users know exactly who they're communicating with online and whether that entity can be trusted with their data.
* When a CA issues a digital certificate for a website, users know they are connected with an official website, not a fake or spoofed website created by a hacker to steal their information or money.

## Install OpenSSL in Windows

* Download [OpenSSL](https://sourceforge.net/projects/openssl-for-windows/) for windows (from any online source)
* Open System Environment Variables prompt
* Go to Path
* Set the full path of `openssl` (example: "C:\Program Files\OpenSSL-Win64\bin" )
* Note: sometimes `openssl.exe` file may be found in root folder and sometimes it can be found in `bin` folder. We have to set the full path of `.exe` file.
* Run Command Prompt or Powershell .
* Write `openssl version` and it will show the current version of openssl which has been installed.


## Generate CA

Open `PowerSell` or `Command Prompt` and write the command that are give below,

* Generate RSA

> openssl genrsa -aes256 -out ca-key.pem 4096

* Generate a public CA Cert 

> openssl req -new -x509 -sha256 -days 365 -key ca-key.pem -out ca.pem

Now we can check our destination folder where we will see these two files has been created, `ca.pem` , `ca-key.pem`.

To see what is inside the CA file we can write the following command in `PowerShell` or `Command Prompt` 

> openssl x509 -in ca.pem -text

## Generate Certificate

Open `PowerShell` or `Command Prompt` and write the command that are given below

* Create a RSA key

> openssl genrsa -out cert-key.pem 4096

* Create a Certificate Signing Request

> openssl req -new -sha256 -subj "/CN=yourcn" -key cert-key.pem -out cert.csr

* Create the certificate

> openssl x509 -req -sha256 -days 365 -in cert.csr -CA ca.pem -CAkey ca-key.pem -out cert.pem -CAcreateserial

## Verify Certificates

> openssl verify -CAfile ca.pem -verbose cert.pem

## Certificate Formats

X.509 Certificates exist in, 

### base64 Formats

* **PEM (.pem, .crt, .ca-bundle)**, **PKCS#7 (.p7b, p7s)** 

### Binary Formats 

* **DER (.der, .cer)**, **PKCS#12 (.pfx, p12)**.

## Convert Certs

COMMAND | CONVERSION
---|---
`openssl pkcs12 -inkey cert-key.pem -in cert.pem -export -out example.pfx` | PEM to PFX
`openssl x509 -outform der -in cert.pem -out cert.der` | PEM to DER
`openssl x509 -inform der -in cert.der -out cert.pem` | DER to PEM
`openssl pkcs12 -in cert.pfx -out cert.pem -nodes` | PFX to PEM

## Links

https://www.youtube.com/watch?v=VH4gXcvkmOY&t=1219s

https://github.com/xcad2k/cheat-sheets/blob/main/misc/ssl-certs.md
