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
        hits = 0,
    }

    load()
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

    load()
end

function love.keypressed(key)
    if key == 'escape' then
        love.event.quit()
    end
end

function load()
    player.load()
    enemy.load()
    ball.load()
end

function checkCollision(a, b)
    return a.x < b.x + b.width and
        b.x < a.x + a.width and
        a.y < b.y + b.height and
        b.y < a.y + a.height
end

function checkPaddleBoundaries(a)
    if a.y < 0 then
        a.y = 0
    elseif a.y + a.height > screen.height then
        a.y = screen.height - a.height
    end
end
