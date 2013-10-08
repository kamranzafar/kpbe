Overview
--------

kpbe is a cross-platform command line PBE tool for files, and is based on bouncycastle encryption algorithms. It can be used to encrypt and password protect files using standard encryption algorithms like AES, RC4, RC2, Triple DES, Blowfish and Twofish.

Examples
--------

Encrypt a file using 128 bit AES. This will use SHA1 as default digest algorithm to produce the key. 
<pre><code>
 kpbe -e -a AES -k 128 -p mypassword -o outdir MySecretFile.doc
</code></pre>

Encrypt a file using 192 bit Triple DES with SHA-512 digest for key.<pre><code>
 kpbe -e -a DES -k 192 -d SHA512 -p mypassword -o outdir MySecretFile.doc
</code></pre>

Decrypt a file encrypted with 128 bit AES<pre><code>
 kpbe -a AES -k 128 -p mypassword -o outdir MySecretFile.doc
</code></pre>

Decrypt a file encrypted with 192 bit Triple DES with SHA-512 digest.<pre><code>
 kpbe -a DES -k 192 -d SHA512 -p mypassword -o outdir MySecretFile.doc
</code></pre>

Options
-------

Below is the list of all the supported command line options<pre><code>
 -a, --algo=VALUE Encryption algorithm (AES, RC4, RC2, DES, BLOWFISH, TWOFISH) 
 -m, --mode=VALUE Block cipher mode (NONE, CBC, CTR, CFB, OFB) 
 -b, --padding=VALUE Block padding (NONE, PKCS7, ISO10126d2, ISO7816d4, X932, ZEROBYTE) 
 -p, --password=VALUE Encryption password 
 -k, --keysize=VALUE Key size 
 -d, --digest=VALUE Digest algorithm (SHA1, SHA224, SHA256, SHA384, SHA512, MD2, MD4, MD5) 
 -s, --salt=VALUE Salt phrase 
 -i, --iterations=VALUE Number of iterations 
 -e, --encrypt Encrypt 
 -t, --type=VALUE Type (PKCS12, OPENSSL) 
 -o, --output=VALUE Output directory 
 -h, --help Help
</code></pre>

Some default values used if not specified.<pre><code>
 Key Size = 128 
 Digest Algorithm = SHA1 
 Mode = CBC 
 Padding = PKCS7 (with modes \`CTR\`, \`CFB\` and \`OFB\` no padding is used) 
 Salt Phrase = This is a long constant phrase used as salt to create PBE key 
 Iterations = 100 
 Type = PKCS12
</code></pre>

Requirements
------------

kpbe requires [.NET 3.5](http://www.microsoft.com/downloads/en/details.aspx?FamilyId=333325fd-ae52-4e35-b531-508d977d32a6&displaylang=en "http://www.microsoft.com/downloads/en/details.aspx?FamilyId=333325fd-ae52-4e35-b531-508d977d32a6&displaylang=en") or later on Windows, or [Mono 2.8](http://www.go-mono.com/mono-downloads/download.html "http://www.go-mono.com/mono-downloads/download.html") or later on Linux/Windows.

Resources
---------

[[1]](http://bouncycastle.org)
