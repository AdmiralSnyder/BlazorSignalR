﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;

namespace BlazorSignalR
{
    public class BlazorHttpConnectionOptions
    {
        private IDictionary<string, string> _headers;

        public BlazorHttpConnectionOptions()
        {
            _headers = new Dictionary<string, string>();
            Transports = HttpTransports.All;
            Implementations = BlazorTransportType.JsWebSockets | BlazorTransportType.ManagedWebSockets |
                              BlazorTransportType.JsServerSentEvents | BlazorTransportType.ManagedServerSentEvents |
                              BlazorTransportType.ManagedLongPolling;
        }

        /// <summary>
        /// Gets or sets a delegate for wrapping or replacing the <see cref="P:Microsoft.AspNetCore.Http.Connections.Client.HttpConnectionOptions.HttpMessageHandlerFactory" />
        /// that will make HTTP requests.
        /// </summary>
        public Func<HttpMessageHandler, HttpMessageHandler> HttpMessageHandlerFactory { get; set; }

        /// <summary>
        /// Gets or sets a collection of headers that will be sent with HTTP requests.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get { return this._headers; }
            set
            {
                IDictionary<string, string> dictionary = value;
                _headers = dictionary ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Gets or sets the URL used to send HTTP requests.</summary>
        public Uri Url { get; set; }

        /// <summary>
        /// Configuration for BlazorSignalR to work on Server-Side Blazor
        /// </summary>
        /// <param name="uriHelper">must be [Inject]ed into the component</param>
        public void UseServerSide(IUriHelper uriHelper)
        {
            UriHelper = uriHelper;
            IsServerSide = true;
        }

        /// <summary>This is the injected UriHelper that we need for running Server-Side</summary>
        public IUriHelper UriHelper { get; private set; }
        /// <summary>Are we running Server-side?</summary>
        public bool IsServerSide { get; private set; }

        /// <summary>
        /// Gets or sets a bitmask comprised of one or more <see cref="T:Microsoft.AspNetCore.Http.Connections.HttpTransportType" /> that specify what transports the client should use to send HTTP requests.
        /// </summary>
        public HttpTransportType Transports { get; set; }

        /// <summary>
        /// Gets or sets a bitmask comprised of one or more <see cref="T:BlazorSignalR.BlazorTransportType" /> that specify what transports the client should use to send HTTP requests. Used to select what implementations to use.
        /// </summary>
        public BlazorTransportType Implementations { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether negotiation is skipped when connecting to the server.
        /// </summary>
        /// <remarks>
        /// Negotiation can only be skipped when using the <see cref="F:Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets" /> transport.
        /// </remarks>
        public bool SkipNegotiation { get; set; }

        /// <summary>
        /// Gets or sets an access token provider that will be called to return a token for each HTTP request.
        /// </summary>
        public Func<Task<string>> AccessTokenProvider { get; set; }
    }
}