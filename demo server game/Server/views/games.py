from models.general import App
from util import view
from util.shared import BaseHandler

__author__ = "Matthew Miner"
__copyright__ = "Copyright 2012, Rebel Hippo Inc."


class IndexHandler(BaseHandler):
    """Displays the user's list of games."""
    def get(self):
        user = users.get_current_user()
        qry = App.query()
        qry = qry.filter(App.owner == user)
        qry = qry.order(-App.created)
        apps = qry.fetch(1000)
        view.render(self, 'games.html', apps=apps)


class AddGameHandler(BaseHandler):
    """Adds a new game."""
    def post(self):
        user = users.get_current_user()

        if not user:
            return

        app_id, secret_key = generate_keys()
        app = App(id=app_id,
                  owner=user,
                  secret_key=secret_key)

        future = app.put_async()
        send_secret_key(user, secret_key)
        future.get_result()  # Ensure put operation completes.
        self.redirect('/games/%s/settings' % app_id)


class MaintenanceHandler(BaseHandler):
    def get(self, *args):
        view.render(self, 'maintenance.html')
