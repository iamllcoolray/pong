ball = {}

function ball.load()
    ball.width = 20
    ball.height = 20
    ball.radius = 10
    ball.speedX = randomStartDirection()
    ball.speedY = randomStartDirection()
    ball.velocity = 1
    ball.startX = (screen.width - ball.width) / 2
    ball.startY = (screen.height - ball.height) / 2
    ball.x = ball.startX
    ball.y = ball.startY
end

function ball.update(dt)
    ball.x = ball.x + ball.speedX * ball.velocity * dt
    ball.y = ball.y + ball.speedY * ball.velocity * dt

    if checkCollision(ball, player) then
        ball.speedX = -ball.speedX
        paddle.hits = paddle.hits + 1
    end

    if checkCollision(ball, enemy) then
        ball.speedX = -ball.speedX
        paddle.hits = paddle.hits + 1
    end

    checkBallBoundaries(dt)

    if paddle.hits >= 5 then
        if ball.velocity <= 5 then
            ball.velocity = ball.velocity + 0.1
        end
        paddle.hits = 0
    end
end

function ball.draw()
    love.graphics.circle("fill", ball.x, ball.y, ball.width, ball.height)
end

function randomStartDirection()
    if math.random(-2, 1) < 0 then
        return -200
    else
        return 200
    end
end

function checkBallBoundaries(dt)
    if ball.x + ball.radius < 0 then
        ball.x = ball.startX
        ball.y = ball.startY
    elseif ball.x - ball.radius > screen.width then
        ball.x = ball.startX
        ball.y = ball.startY
    end

    if ball.y - ball.radius < 0 then
        ball.speedY = -ball.speedY
    elseif ball.y + ball.radius > screen.height then
        ball.speedY = -ball.speedY
    end
end
