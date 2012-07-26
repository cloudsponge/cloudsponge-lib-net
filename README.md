cloudsponge-lib-net
===================

Simple .Net wrapper for www.cloudsponge.com's REST API.

Forked from Greg Dean's original contribution at http://cloudsponge.codeplex.com/.

Usage Example
-------------

    // Register your domain at http://www.cloudsponge.com.
    // instantiate with your CloudSponge credentials
    var api = new Api("DomainKey", "DomainPassword");
    var consent = api.Consent(ContactSource.Gmail);
    Process.Start(consent.Url);
    var events = api.Events(consent.ImportId);
    // Wait for events.IsComplete
    var contacts = api.Contacts(consent.ImportId);

License
=======

The MIT License (MIT)

Copyright (c) 2010 Greg Dean

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

