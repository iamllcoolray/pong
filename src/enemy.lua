enemy = {}

function enemy.load()
    enemy.width = paddle.width
    enemy.height = paddle.height
    enemy.x = enemy.width - 20
    enemy.y = (screen.height - enemy.height) / 2
    enemy.score = 0
    enemy.speed = paddle.speed
end

function enemy.update(dt)
    checkPaddleBoundaries(enemy)
end

function enemy.draw()
    love.graphics.rectangle("fill", enemy.x, enemy.y, enemy.width, enemy.height)
end
