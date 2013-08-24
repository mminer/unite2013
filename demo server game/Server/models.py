from google.appengine.ext import ndb

__copyright__ = "Copyright 2012, Rebel Hippo Inc."


class LeaderboardEntry(ndb.Model):
    date = ndb.DateProperty(auto_now=True, indexed=True)
    score = ndb.IntegerProperty(required=True, indexed=True)
    name = ndb.StringProperty(required=True)
    leaderboard_id = ndb.StringProperty(required=True, indexed=True)
