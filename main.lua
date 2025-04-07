love.graphics.setDefaultFilter('nearest', 'nearest')

require "src.player"
require "src.enemy"
require "src.ball"

function love.load()
    screen = {
        width = love.graphics.getWidth(),
        height = love.graphics.getHeight(),
    }

    paddle = {
        width = 40,
        height = 120,
        speed = 200,
    }

    player.load()
    enemy.load()
    ball.load()
end

function love.update(dt)
    player.update(dt)
    enemy.update(dt)
    ball.update(dt)
end

function love.draw()
    player.draw()
    enemy.draw()
    ball.draw()
end

function love.resize(w, h)
    screen.width = w
    screen.height = h
end

function love.keypressed(key)
    if key == 'escape' then
        love.event.quit()
    end
end
