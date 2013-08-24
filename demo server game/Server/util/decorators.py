"""Method decorators for modifying incoming information."""

from google.appengine.api import namespace_manager
from google.appengine.api import users
from models.general import App
from . import view

__author__ = "Matthew Miner"
__copyright__ = "Copyright 2012, Rebel Hippo Inc."


def authorization_required(func):
    """Ensures that the user requesting the app's info is allowed to do so."""
    def verify_ownership(handler, app_id, *args):
        app = App.get_by_id(app_id)
        user = users.get_current_user()

        if app is None:
            view.render_error(handler, 404)
        elif user == app.owner or users.is_current_user_admin():
            return func(handler, app, app_id, *args)
        else:
            view.render_error(handler, 403)

    return verify_ownership


def namespaced(func):
    """Sets datastore namespace to the app ID, changing it back afterwards."""
    def set_namespace(handler, app, app_id, *args):
        # Temporarily set the namespace.
        namespace = namespace_manager.get_namespace()

        try:
            namespace_manager.set_namespace(app_id)
            return func(handler, app, app_id, *args)
        finally:
            # Restore the saved namespace.
            namespace_manager.set_namespace(namespace)

    return set_namespace
