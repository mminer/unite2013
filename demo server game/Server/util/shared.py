"""Basic classes that are used throughout the application."""

import logging
import webapp2
from protorpc import messages
from util import view

__author__ = "Matthew Miner"
__copyright__ = "Copyright 2012, Rebel Hippo Inc."


class BaseHandler(webapp2.RequestHandler):
    """A catch-all errors request handler."""
    def handle_exception(self, exception, debug_mode):
        if debug_mode:
            webapp2.RequestHandler.handle_exception(self, exception,
                                                    debug_mode)
        else:
            logging.exception(exception)
            view.render_error(self, 500)


class BasicRequest(messages.Message):
    """A simple unauthenticated request."""
    app_id = messages.StringField(1, required=True)


class BasicAuthenticatedRequest(messages.Message):
    """A simple authenticated request."""
    app_id = messages.StringField(1, required=True)
    secret_key = messages.StringField(2, required=True)


class BasicResponse(messages.Message):
    """A simple string response."""
    result = messages.StringField(1, required=True)
