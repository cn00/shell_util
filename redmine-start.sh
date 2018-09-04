#!/usr/bin/env sh

cd /usr/share/redmine

sudo bundle exec rails server webrick -e production

ssh -N -R 3300:localhost:3000 a3@10.23.22.233