player = {}

function player.load()
    player.width = paddle.width
    player.height = paddle.height
    player.x = (screen.width - player.width) - 20
    player.y = (screen.height - player.height) / 2
    player.score = 0
    player.speed = paddle.speed
end

function player.update(dt)
    player.movement(dt)
end

function player.draw()
    love.graphics.rectangle("fill", player.x, player.y, player.width, player.height)
end

function player.movement(dt)
    if love.keyboard.isDown("up") then
        player.y = player.y - player.speed * dt
    end

    if love.keyboard.isDown("down") then
        player.y = player.y + player.speed * dt
    end
end
