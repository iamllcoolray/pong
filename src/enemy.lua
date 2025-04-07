enemy = {}

function enemy.load()
    enemy.x = paddle.width - 20
    enemy.y = (screen.height - paddle.height) / 2
end

function enemy.update(dt)

end

function enemy.draw()
    love.graphics.rectangle("fill", enemy.x, enemy.y, paddle.width, paddle.height)
end
