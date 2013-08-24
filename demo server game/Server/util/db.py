"""Functions for fetching specific entities from the datastore."""

from google.appengine.ext import ndb
from datetime import datetime, time, timedelta
from models.general import Account, Player
from . import counters, mailchimp

__author__ = "Matthew Miner"
__copyright__ = "Copyright 2012, Rebel Hippo Inc."


def count_players(app_id, day):
    """Counts the player entities from a specific day."""
    # Filter by date.
    single_day = timedelta(days=1)
    lower_bound = datetime.combine(day, time())
    upper_bound = datetime.combine(day + single_day, time())

    qry = Player.query(namespace=app_id)
    qry = qry.filter(Player.created >= lower_bound)
    qry = qry.filter(Player.created < upper_bound)

    # Potentially dangerous for popular games.
    count = qry.count(limit=None)
    return count


def get_or_insert_account(user, save=True):
    """
    Returns account settings of current user or creates default settings.
    """
    qry = Account.query()
    qry = qry.filter(Account.owner == user)
    account = qry.get()

    if account is None:
        # Account entity doesn't actually exist yet, so create it.
        account = Account(owner=user)
        mailchimp.subscribe(user.email())

        if save:
            account.put()

    return account


def get_or_insert_player(app_id, player_id, save=True):
    """Returns the player with the given ID or creates a new one."""
    if not player_id:
        return None

    player = Player.get_by_id(player_id, namespace=app_id)

    if player is None:
        # Create a new player and increment the total players counter.
        key = ndb.Key(Player, player_id, namespace=app_id)
        player = Player(key=key)
        counters.increment('players', namespace=app_id)

        if save:
            player.put()

    return player
