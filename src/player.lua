player = {}

function player.load()
    player.x = (screen.width - paddle.width) - 20
    player.y = (screen.height - paddle.height) / 2
end

function player.update(dt)
    player.movement(dt)
end

function player.draw()
    love.graphics.rectangle("fill", player.x, player.y, paddle.width, paddle.height)
end

function player.movement(dt)
    if love.keyboard.isDown("up") then
        player.y = player.y - paddle.speed * dt
    end

    if love.keyboard.isDown("down") then
        player.y = player.y + paddle.speed * dt
    end
end
